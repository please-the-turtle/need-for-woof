class_name DogStateJump
extends DogState


const _in_air_collisions = 0b1010
const _on_ground_collisions = 0b1100

var _initial_z_index: int

func on_enter(_msg := {}) -> void:
	target.stamina -= target.jump_stamina_expenditure
	target.speed_multiplier *= target.jumping_speed_boost
	target.collision_mask = _in_air_collisions
	_initial_z_index = target.z_index
	target.z_index = 5
	target.animation.play("jump")


func physics_update(_delta) -> void:
	target.velocity = Vector2.UP * target.speed * target.speed_multiplier
	target.move_and_slide()


func on_exit() -> void:
	target.collision_mask = _on_ground_collisions
	target.speed_multiplier /= target.jumping_speed_boost
	target.z_index = _initial_z_index
