extends Control


@onready var players_list: VBoxContainer = $PlayersList
@onready var players_manager: PlayersManager = PlayersManager.new()
@onready var player_info_scene = preload("res://scenes/ui/player_info.tscn")


func _ready():
	$RoomIdLabel.text = "ID: %s" % ServerClient.room_id
	
	players_manager.player_joined.connect(_on_player_joined)
	players_manager.player_left.connect(_on_player_left)


func _on_player_joined(player: Player):
	print("On player joined")
	var player_info = player_info_scene.instantiate()
	player_info.name = player.id
	players_list.add_child(player_info)
	print(players_list.get_children())


func _on_player_left(player: Player):
	print("On player left")
	var player_info = players_list.find_child(player.id, false, false)
	print(player.id, "\n", player_info)
	if player_info != null:
		players_list.remove_child(player_info)
		player_info.queue_free()
	


func _on_copy_button_pressed():
	var id_string = $RoomIdLabel.text
	id_string = id_string.trim_prefix("ID: ")
	DisplayServer.clipboard_set(id_string)


func _on_back_button_pressed():
	ServerClient.leave_room()
