extends Control


@onready var players_list: VBoxContainer = $PlayersList
var players_manager: PlayersManager = PlayersManager.new()
@onready var player_info_scene = preload("res://scenes/ui/player_info.tscn")


func _ready():
	$RoomIdLabel.text = "ID: %s" % ServerClient.room_id
	
	var local_player_info = player_info_scene.instantiate()
	local_player_info.name = players_manager.local_player.id
	players_list.add_child(local_player_info)
	local_player_info.update_info.call_deferred(players_manager.local_player)
	
	players_manager.player_joined.connect(_on_player_joined)
	players_manager.player_left.connect(_on_player_left)
	players_manager.player_ready_changed.connect(_on_player_ready_changed)
	players_manager.player_nickname_changed.connect(_on_player_nickname_changed)
	
	players_manager.introduce_yourself()


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


func _on_copy_button_pressed():
	var id_string = $RoomIdLabel.text
	id_string = id_string.trim_prefix("ID: ")
	DisplayServer.clipboard_set(id_string)


func _on_back_button_pressed():
	players_manager.free()
	ServerClient.leave_room()


func _on_ready_button_toggled(toggled_on):
	var error = players_manager.set_local_player_ready(toggled_on)
	if error == OK:
		var local_player_info = players_list.find_child(players_manager.local_player.id, false, false)
		local_player_info.player_ready = toggled_on
