class_name DogStateMachine
extends StateMachine


@export var target: Dog


func jump() -> void:
	state.jump()


func run_left() -> void:
	state.run_left()


func run_right() -> void:
	state.run_right()


func push(impulse: Vector2) -> void:
	state.push(impulse)

