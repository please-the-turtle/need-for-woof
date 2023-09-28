## The state in which the dog bounces according 
## to the impulse transmitted to it. 
## The pulse value is transmitted as a message when switching 
## from another state in a pair "impulse":Vector2
class_name DogStateBounce
extends DogState

## The greater the impulse attenuation value, 
## the less time the bounce lasts.
@export_range(1, 20, 0.5) var impulse_attenuation: float = 5

## Multiplies initial bouncing impulse 
@export_range(0, 10, 0.5) var bounce_power: float = 2


var _bounce_velocity = null


func on_enter(_msg := {}) -> void:
	var impulse = _msg.get("impulse")
	if impulse == null or not impulse is Vector2:
		printerr("DogStateBounce: Invalid bounce impulse value")
		impulse = Vector2.DOWN
	
	_bounce_velocity = impulse * bounce_power
	target.animation.pause()


func physics_update(_delta) -> void:
	if _bounce_velocity == null:
		return
	
	if _bounce_velocity.length() <= 500:
		target.fsm.transition_to("DogStateRun")
		return
	
	target.velocity = _bounce_velocity
	target.move_and_slide()
	var t = impulse_attenuation * _delta
	_bounce_velocity = _bounce_velocity.lerp(Vector2.ZERO, t)


func on_exit() -> void:
	_bounce_velocity = null
