class_name LevelGUI
extends CanvasLayer


signal home_button_pressed
signal restart_button_toggled(toggled_on: bool)


@export var target: Dog:
	get: 
		return target
	set(value):
		target = value
		set_process(value != null)


@onready var stamina_bar: TextureProgressBar = %StaminaBar


func _ready(): 
	set_process(target != null)


func show_dog_controls():
	$DogControls.visible = true
	$FinishedButtons/RestartButton.button_pressed = false
	$FinishedButtons.visible = false


func show_finished_buttons():
	$DogControls.visible = false
	$FinishedButtons.visible = true


func _process(_delta):
	stamina_bar.value = target.stamina / target.max_stamina * 100


func _on_home_button_pressed():
	home_button_pressed.emit()


func _on_restart_button_toggled(toggled_on):
	restart_button_toggled.emit(toggled_on)
