extends OffsetContainer


const DISPLAY_MESSAGE_TIME = 2.0


@onready var animation = $AnimationPlayer 
@onready var label = $MessageLabel


func _ready():
	visible = false


func show_message(message: String):
	if not visible:
		_show_message(message)
		return
	
	await visibility_changed
	show_message.call_deferred(message)


func _show_message(message: String):
	label.text = message
	animation.play("show_message")
	visible = true
	await get_tree().create_timer(DISPLAY_MESSAGE_TIME).timeout
	visible = false
