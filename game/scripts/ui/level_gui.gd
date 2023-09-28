extends CanvasLayer


@export var target: Dog


@onready var stamina_bar: TextureProgressBar = %StaminaBar


func _process(_delta):
	stamina_bar.value = target.stamina / target.max_stamina * 100
