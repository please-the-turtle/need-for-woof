class_name LevelGUI
extends CanvasLayer


signal home_button_pressed
signal restart_button_toggled(toggled_on: bool)


@export var target: Dog


@onready var stamina_bar: TextureProgressBar = %StaminaBar


func show_dog_controls():
	$DogControls.visible = true
	$FinishedButtons.visible = false


func show_finished_buttons():
	$DogControls.visible = false
	$FinishedButtons/RestartButton.button_pressed = false
	$FinishedButtons.visible = true


func _process(_delta):
	stamina_bar.value = target.stamina / target.max_stamina * 100


func _on_home_button_pressed():
	SceneChanger.go_to_scene("res://scenes/ui/main_menu.tscn")
	home_button_pressed.emit()


func _on_restart_button_toggled(toggled_on):
	restart_button_toggled.emit(toggled_on)
