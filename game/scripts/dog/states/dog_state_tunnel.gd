class_name DogStateTunnel
extends DogState


## Multiplies the speed of the dog that is in the tunnel.
@export_range(1, 10, 0.5, "or_greater") var speed_bonus: float = 2.0


func on_enter(_msg := {}) -> void:
	target.speed_multiplier *= speed_bonus
	target.animation.play("run")


func physics_update(_delta) -> void:
	target.velocity = Vector2.UP * target.speed * target.speed_multiplier
	target.move_and_slide()


func on_exit() -> void:
	target.speed_multiplier /= speed_bonus
