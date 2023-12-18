class_name PlayersManager
extends Object


signal player_joined(player: Player)
signal player_left(player: Player)
signal player_ready_changed(player: Player)
signal player_nickname_changed(player: Player)
signal player_position_changed(player_id: String, new_position: Vector2)
signal player_animation_changed(player_id: String, animation_name: String)


## Separates the sender's ID from the message text
const MESS_DELIMETER = ":"

## Separates the name of the command from its set of parameters
const COMMAND_DELIMETER = "="

const COMMAND_JOINDED = "JOINED"
const COMMAND_LEFT = "LEFT"
const COMMAND_READY = "ready"
const COMMAND_NICKNAME = "nickname"
const COMMAND_DOG_TYPE = "dog"
const COMMAND_POSITION = "pos"
const COMMAND_ANIMATION = "anim"


## Data of local player
var local_player: Player = null

## Dictionary id:Player with data of connected players
var remote_players: Dictionary = {}

## MUST store pairs str:callable(caller_id: String, params: String)
## Used in parsing messages for handle message commands
var _command_handlers: Dictionary = {
	COMMAND_JOINDED: _join_handler,
	COMMAND_LEFT: _left_handler,
	COMMAND_READY: _ready_handler,
	COMMAND_NICKNAME: _nickname_handler,
	COMMAND_POSITION: _position_handler,
	COMMAND_ANIMATION: _animation_handler,
}


func _init():
	local_player = Player.new()
	local_player.id = ServerClient.client_id
	local_player.nickname = GameSettings.get_setting(GameSettings.NICKNAME)

	ServerClient.connected.connect(on_server_connected)
	ServerClient.disconnected.connect(on_server_disconnected)
	ServerClient.message_received.connect(on_server_message)


func on_server_connected():
	local_player.id = ServerClient.client_id
	local_player.nickname = GameSettings.get_setting(GameSettings.NICKNAME)


func on_server_disconnected():
	local_player.id = ""


func on_server_message(message: String):
	# Message structure: '<id>: <command1> <command2>=<params>'
	# Message sample: 'OgtxzRiJub5dw1Y2: JOINED ready=false nickname=andrey'
	var mess_parts = message.split(MESS_DELIMETER, false)
	if mess_parts.size() < 2:
		return
	
	var player_id = mess_parts[0]
	var args = mess_parts[1].split(" ", false)
	for arg in args:
		var command = arg.get_slice(COMMAND_DELIMETER, 0)
		var handler = _command_handlers.get(command)
		if handler == null:
			push_warning("Server message: Unknown command:", command)
			continue
		if handler is Callable:
			var params = arg.trim_prefix(command + COMMAND_DELIMETER)
			handler.call(player_id, params)


## Sends players information on the server
func introduce_yourself():
	# Sample: "JOINED ready=true nickname=NewNick"
	var intro_mess = "%s %s=%s %s=%s" % [
		COMMAND_JOINDED, 
		COMMAND_READY, local_player.ready,
		COMMAND_NICKNAME, local_player.nickname,
	]
	ServerClient.send_tcp(intro_mess)


func set_local_player_ready_for_all(ready: bool) -> Error:
	var ready_message = "%s=%s" % [COMMAND_READY, ready]
	var error = ServerClient.send_tcp(ready_message)
	if error == OK:
		local_player.ready = ready
		player_ready_changed.emit(local_player)
	
	return error


## Sends information about the local player's position to the remote players.
func send_local_player_position(position: Vector2) -> Error:
	# Sample: pos=200,240
	var position_message = "%s=%s,%s" % [COMMAND_POSITION, position.x, position.y]
	return ServerClient.send_udp(position_message)


## Sends information about the local player's animation to the remote players.
func send_local_player_animation(animation_name: String) -> Error:
	# Sample: anim=idle
	var animation_message = "%s=%s" % [COMMAND_ANIMATION, animation_name]
	return ServerClient.send_tcp(animation_message)


func is_all_players_ready() -> bool:
	if local_player and not local_player.ready:
		return false
	
	for player in remote_players.values():
		if not(player is Player):
			return false
		var p = player as Player
		if not p.ready:
			return false
	
	return true


func _join_handler(caller_id: String, _params: String):
	if remote_players.has(caller_id):
		return
	
	var player = Player.new(caller_id)
	remote_players[caller_id] = player
	player_joined.emit(player)
	introduce_yourself()


func _left_handler(caller_id: String, _params: String):
	var player = remote_players.get(caller_id)
	if player != null:
		remote_players.erase(caller_id)
		player_left.emit(player)


func _ready_handler(caller_id: String, params: String):
	var player = remote_players.get(caller_id)
	if player != null:
		player.ready = true if params.contains("true") else false 
		player_ready_changed.emit(player)


func _nickname_handler(caller_id: String, params: String):
	var player = remote_players.get(caller_id)
	if player != null:
		player.nickname = params
		player_nickname_changed.emit(player)


func _position_handler(caller_id: String, params: String):
	if not remote_players.has(caller_id):
		return
	
	var position_dimentions = params.split_floats(",", false)
	if position_dimentions.size() < 2:
		return
	
	var new_position = Vector2(position_dimentions[0], position_dimentions[1])
	player_position_changed.emit(caller_id, new_position)


func _animation_handler(caller_id: String, params: String):
	if not remote_players.has(caller_id):
		return
	
	if params.is_empty():
		return
	
	player_animation_changed.emit(caller_id, params)
