extends Control


@onready var players_list: VBoxContainer = $PlayersList
@onready var players_manager: PlayersManager = PlayersManager.new()


func _ready():
	$RoomIdLabel.text = "ID: %s" % ServerClient.room_id


func _on_back_button_pressed():
	ServerClient.leave_room()
