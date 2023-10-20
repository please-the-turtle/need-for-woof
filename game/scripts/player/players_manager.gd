class_name PlayersManager
extends Object


const MESS_DELIMETER = ":"
const COMMAND_DELIMETER = "="
const COMMAND_JOINDED = "JOINED"
const COMMAND_LEFT = "LEFT"
const COMMAND_READY = "ready"


## Data of local player
var local_player: Player = null

## Dictionary id:Player with data of connected players
var remote_players: Dictionary = {}

## MUST store pairs str:callable(caller_id: String, params: String)
## Used in parsing messages for handle message commands
var _command_handlers: Dictionary = {
	COMMAND_JOINDED: _join_handler,
	COMMAND_LEFT: _left_handler,
}

func _init():
	local_player = Player.new()

	ServerClient.connected.connect(on_server_connected)
	ServerClient.disconnected.connect(on_server_disconnected)
	ServerClient.message_received.connect(on_server_message)


func on_server_connected():
	local_player.id = ServerClient.client_id
	local_player.nickname = GameSettings.get_setting(GameSettings.NICKNAME)


func on_server_disconnected():
	local_player.id = ""


func on_server_message(message: String):
	var mess_parts = message.split(MESS_DELIMETER, false)
	if mess_parts.size() < 2:
		return
	
	var player_id = mess_parts[0]
	var args = mess_parts[1].split(" ", false)
	for arg in args:
		var command = arg.get_slice(COMMAND_DELIMETER, 0)
		var handler = _command_handlers.get(command)
		if handler == null:
			continue
		if handler is Callable:
			var params = arg.get_slice(COMMAND_DELIMETER, 1)
			handler.call(player_id, params)


func _join_handler(caller_id: String, params: String):
	print("join handler called by " + caller_id + " with params: ", params)


func _left_handler(caller_id: String, params: String):
	print("left handler called by " + caller_id + " with params: ", params)
