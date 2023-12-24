class_name DogStateIdle
extends DogState


func on_enter(_msg := {}) -> void:
	target.animation.play("idle")


func push(impulse: Vector2) -> void:
	fsm.transition_to("DogStateBounce", {
		"impulse": impulse,
		"release_state": "DogStateIdle",
	})
