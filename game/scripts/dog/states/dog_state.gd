class_name DogState
extends State


var target: Dog:
	get:
		return fsm.target


func jump() -> void:
	pass


func run_left() -> void:
	pass


func run_right() -> void:
	pass


func push(_impulse: Vector2) -> void:
	pass
