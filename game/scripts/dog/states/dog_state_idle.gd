class_name DogStateIdle
extends DogState


func on_enter(_msg := {}) -> void:
	target.animation.play("idle")
	pass
