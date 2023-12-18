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
	_players_manager.player_ready_changed.connect(_on_player_ready_changed)
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


func _on_player_ready_changed(player: Player):
	var player_info = players_list.find_child(player.id, false, false)
	if player_info == null:
		return
	
	player_info.player_ready = player.ready
	if _players_manager.is_all_players_ready():
		_players_manager.local_player.ready = false
		for remote_player in _players_manager.remote_players.values():
			remote_player.ready = false
		SharedStorage.put(SharedStorage.PLAYERS_MANAGER, _players_manager)
		SceneChanger.go_to_scene("res://scenes/level/multiplayer_scene.tscn")


func _on_ready_button_toggled(toggled_on):
	_players_manager.set_local_player_ready_for_all(toggled_on)


func _on_copy_button_pressed():
	if not DisplayServer.has_feature(DisplayServer.FEATURE_CLIPBOARD):
		return
	
	var id_string = $RoomIdLabel.text
	id_string = id_string.trim_prefix("ID: ")
	DisplayServer.clipboard_set(id_string)
	$RoomIdLabel/CopyButton/CopiedIcon.visible = true


func _on_back_button_pressed():
	_players_manager.free()
	ServerClient.leave_room()
