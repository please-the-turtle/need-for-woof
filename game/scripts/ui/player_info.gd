extends HBoxContainer


#@onready var _icon_texture = $DogIcon
@onready var _ready_icon = $ReadyIcon
@onready var _not_ready_icon = $NotReadyIcon
@onready var _nickname_label = $Nickname


func _ready():
	_ready_icon.visible = false
	_not_ready_icon.visible = true
	_nickname_label.text = ""


var player_ready: bool = false:
	get:
		return player_ready
	set(value):
		player_ready = value
		_ready_icon.visible = player_ready
		_not_ready_icon.visible = !player_ready


var nickname: String = "":
	get:
		return nickname
	set(value):
		nickname = value
		_nickname_label.text = nickname


func update_info(player: Player):
	nickname = player.nickname
	player_ready = player.ready
