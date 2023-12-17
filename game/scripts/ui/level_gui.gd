extends CanvasLayer


signal home_button_pressed


@export var target: Dog


@onready var stamina_bar: TextureProgressBar = %StaminaBar


func _process(_delta):
	stamina_bar.value = target.stamina / target.max_stamina * 100


func _on_home_button_pressed():
	SceneChanger.go_to_scene("res://scenes/ui/main_menu.tscn")
	home_button_pressed.emit()
