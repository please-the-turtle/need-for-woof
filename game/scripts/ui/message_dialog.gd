extends PanelContainer


signal ok_pressed


var header: String = "Ooops!":
	get:
		return $VBoxContainer/Header.text
	set(value):
		$VBoxContainer/Header.text = value


var message: String = "Something wrong":
	get:
		return $VBoxContainer/Message.text
	set(value):
		$VBoxContainer/Message.text = value


var back_on_ok: bool = false


## Used for position animation
@export var position_offset: Vector2 = Vector2.ZERO:
	get: 
		return position_offset
	set(value):
		position -= position_offset
		position_offset = value
		position += position_offset


@onready var _animation: AnimationPlayer = $AnimationPlayer


func _ready():
	visible = false


func open():
	_animation.play("open")


func close():
	_animation.play("close")


func _on_ok_button_pressed():
	close()
	emit_signal("ok_pressed")
	if back_on_ok:
		SceneChanger.go_to_previous_scene()
