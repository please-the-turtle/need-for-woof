class_name DogStateJump
extends DogState


var _initial_z_index: int


func on_enter(_msg := {}) -> void:
	target.stamina -= target.jump_stamina_expenditure
	target.speed_multiplier *= target.jumping_speed_boost
	_initial_z_index = target.z_index
	target.z_index = 5
	target.animation.play("jump")


func physics_update(_delta) -> void:
	target.velocity = Vector2.UP * target.speed * target.speed_multiplier
	target.move_and_slide()
	
	_push_another_dogs()


func on_exit() -> void:
	target.collision_mask = PlayerCollisionMasks.ON_GROUND_COLLISIONS
	target.speed_multiplier /= target.jumping_speed_boost
	target.z_index = _initial_z_index


func _push_another_dogs() -> void:
	var coll_count = target.get_slide_collision_count()
	if coll_count < 1:
		return
	
	for i in coll_count:
		var collision = target.get_slide_collision(i)
		var collider = collision.get_collider()
		
		if not collider is Dog:
			continue
		
		var bounce_impulse: Vector2 = collision.get_normal()
		bounce_impulse *= -1
		bounce_impulse *= target.speed * target.speed_multiplier
		collider.fsm.transition_to("DogStateBounce", {
			"impulse": bounce_impulse
		})
		target.fsm.transition_to("DogStateRun")


func _on_animation_finished(anim_name: String):
	if anim_name != "jump":
		return
	target.fsm.transition_to("DogStateRun")


