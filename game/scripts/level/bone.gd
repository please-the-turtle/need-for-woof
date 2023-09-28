extends Area2D


@export_range(1, 500, 10, "or_greater") var stamina_bonus: float = 50.0


@onready var animation = $AnimationPlayer


func _ready():
	animation.play("idle")


func _on_body_entered(body: Node2D):
	if body is Dog:
		body.stamina += stamina_bonus
		animation.play("picked")
