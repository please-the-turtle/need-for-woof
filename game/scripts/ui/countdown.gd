extends Control


signal is_over


const TICK_LABELS = [" 3", " 2", " 1"]
const START_LABEL = " GO!"


@onready var label: Label = $Label
@onready var animation: AnimationPlayer = $AnimationPlayer


func _ready():
	visible = false
	label.text = ""


func start():
	visible = true
	for i in TICK_LABELS:
		label.text = i
		animation.play("tick")
		await animation.animation_finished
	emit_signal("is_over")
	label.text = START_LABEL
	animation.play("tick")
	await animation.animation_finished
	visible = false



