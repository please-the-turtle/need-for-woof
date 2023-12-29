extends Control


@onready var players_list: VBoxContainer = $PlayersList
var _players_manager: PlayersManager
@onready var player_info_scene = preload("res://scenes/ui/player_info.tscn")


func _ready():
	$RoomIdLabel.text = "ID: %s" % ServerClient.room_id
	
	_players_manager = SharedStorage.take(SharedStorage.PLAYERS_MANAGER)
	if _players_manager == null:
		_players_manager = PlayersManager.new()
	
	# Adding local player on the players list
	var local_player_info = player_info_scene.instantiate()
	local_player_info.name = _players_manager.local_player.id
	players_list.add_child(local_player_info)
	local_player_info.update_info.call_deferred(_players_manager.local_player)
	
	# Connecting PlayersManager signals
	_players_manager.player_joined.connect(_on_player_joined)
	_players_manager.player_left.connect(_on_player_left)
	_players_manager.player_status_changed.connect(_on_player_status_changed)
	_players_manager.player_nickname_changed.connect(_on_player_nickname_changed)
	
	_players_manager.introduce_yourself()


func _on_player_joined(player: Player):
	var player_info = player_info_scene.instantiate()
	player_info.name = player.id
	players_list.add_child(player_info)


func _on_player_left(player: Player):
	var player_info = players_list.find_child(player.id, false, false)
	if player_info == null:
		return
	
	players_list.remove_child(player_info)
	player_info.queue_free()


func _on_player_nickname_changed(player: Player):
	var player_info = players_list.find_child(player.id, false, false)
	if player_info == null:
		return
	
	player_info.nickname = player.nickname


func _on_player_status_changed(player: Player):
	var player_info = players_list.find_child(player.id, false, false)
	if player_info == null:
		return
	
	player_info.player_ready = _is_player_ready(player)
	if not _is_all_players_ready():
		return
	
	_players_manager.set_local_player_status_for_all(PlayerStatus.ON_LEVEL_NOT_READY)
	SharedStorage.put(SharedStorage.PLAYERS_MANAGER, _players_manager)
	SceneChanger.go_to_scene("res://scenes/level/multiplayer_scene.tscn")


func _is_all_players_ready() -> bool:
	if not _is_player_ready(_players_manager.local_player):
		return false
	
	for player in _players_manager.remote_players.values():
		if not _is_player_ready(player):
			return false
	
	return true


func _is_player_ready(player: Player) -> bool:
	return player.status == PlayerStatus.IN_ROOM_READY \
			|| player.status == PlayerStatus.ON_LEVEL_READY


func _on_ready_button_toggled(toggled_on):
	var status = PlayerStatus.IN_ROOM_READY if toggled_on \
				else PlayerStatus.IN_ROOM_NOT_READY
	_players_manager.set_local_player_status_for_all(status)


func _on_copy_button_pressed():
	if not DisplayServer.has_feature(DisplayServer.FEATURE_CLIPBOARD):
		$Message.show_message("Clipboard not supported")
		return
	
	var id_string = $RoomIdLabel.text
	id_string = id_string.trim_prefix("ID: ")
	DisplayServer.clipboard_set(id_string)
	$RoomIdLabel/CopyButton/CopiedIcon.visible = true
	$Message.show_message("Room ID copied")


func _on_back_button_pressed():
	_players_manager.free()
	ServerClient.leave_room()
