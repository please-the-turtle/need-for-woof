## The state in which the dog bounces according 
## to the impulse transmitted to it. 
## If the dog collides during the bounce, then it gets slowed down.
## The impulse value is transmitted as a message when switching 
## from another state in a pair "impulse":Vector2
## The state that the dog will enter after bouncing can be changed 
## by passing a pair "release_state":String in the message
## If this condition is not specified, then the dog will 
## switch to the DEFAULT_RELEASE_STATE
class_name DogStateBounce
extends DogState


const DEFAULT_RELEASE_STATE = "DogStateRun"


## The greater the impulse attenuation value, 
## the less time the bounce lasts.
@export_range(1, 20, 0.5) var impulse_attenuation: float = 5

## Multiplies initial bouncing impulse 
@export_range(0, 10, 0.5) var bounce_power: float = 1.5

## The degree of slowing down of the character when colliding with obstractions.
@export_range(1, 10, 0.25) var collide_slowing_down: float = 2

## Slowing down time in seconds
@export_range(0.1, 10, 0.1) var slowing_down_time: float = 2


## The name of state that the dog will go into after bouncing off
var _release_state

var _bounce_velocity: Vector2
var _is_slowed: bool = false


func on_enter(_msg := {}) -> void:
	var impulse = _msg.get("impulse")
	if impulse == null or not impulse is Vector2:
		printerr("DogStateBounce: Invalid bounce impulse value")
		impulse = Vector2.DOWN
	
	_release_state = _msg.get("release_state")
	if _release_state == null or not _release_state is String:
		_release_state = DEFAULT_RELEASE_STATE
	
	_bounce_velocity = impulse * bounce_power
	target.animation.pause()


func physics_update(delta) -> void:
	if _bounce_velocity == null:
		return
	
	if _bounce_velocity.length() <= 500:
		target.fsm.transition_to(_release_state)
		return
	
	target.ray_cast.target_position = _bounce_velocity * delta
	target.ray_cast.force_raycast_update()
	if target.ray_cast.is_colliding():
		_bounce_velocity = target.ray_cast.get_collision_point() - target.position
	
	target.velocity = _bounce_velocity
	var collided = target.move_and_slide()
	if collided:
		_slow_down()
		target.fsm.transition_to(_release_state)
		return
	var t = impulse_attenuation * delta
	_bounce_velocity = _bounce_velocity.lerp(Vector2.ZERO, t)


func on_exit() -> void:
	_bounce_velocity = Vector2.ZERO
	target.ray_cast.target_position = Vector2.ZERO


func _slow_down():
	if _is_slowed:
		return
	
	_is_slowed = true
	target.speed_multiplier /= collide_slowing_down
	await get_tree().create_timer(slowing_down_time).timeout
	target.speed_multiplier *= collide_slowing_down
	_is_slowed = false
	
