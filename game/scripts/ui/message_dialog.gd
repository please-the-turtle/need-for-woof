extends OffsetContainer


signal ok_pressed


var header: String = "Ooops!":
	get:
		return %Header.text
	set(value):
		%Header.text = value


var message: String = "Something wrong":
	get:
		return %Message.text
	set(value):
		%Message.text = value


var back_on_ok: bool = false


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
