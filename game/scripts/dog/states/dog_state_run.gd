class_name DogStateRun
extends DogState


var _direction = Vector2.UP


func on_enter(_msg := {}) -> void:
	target.animation.play("run")
	target.collision_mask = DogCollisionMasks.ON_GROUND_COLLISIONS


func physics_update(_delta) -> void:
	_direction = _direction.normalized()
	target.velocity = _direction * target.speed * target.speed_multiplier
	target.move_and_slide()
	_direction = Vector2.UP


func jump() -> void:
	if target.stamina < target.jump_stamina_expenditure:
		return
		
	fsm.transition_to("DogStateJump")
	pass


func run_left() -> void:
	_direction += Vector2.LEFT
	pass


func run_right() -> void:
	_direction += Vector2.RIGHT
	pass


func push(impulse: Vector2) -> void:
	fsm.transition_to("DogStateBounce", {
		"impulse": impulse,
	}) 
