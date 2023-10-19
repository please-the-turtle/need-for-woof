extends Control


signal is_over


const TICK_LABELS = ["3", "2", "1", "GO!"]


@onready var label: Label = $Label
@onready var animation: AnimationPlayer = $AnimationPlayer


func _ready():
	visible = false
	label.text = ""
	
	start()


func start():
	visible = true
	for i in TICK_LABELS:
		label.text = i
		animation.play("tick")
		await animation.animation_finished
	visible = false
	emit_signal("is_over")



