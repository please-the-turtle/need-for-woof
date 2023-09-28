class_name StateMachine
extends Node


## Emitted when transitioning to a new state.
signal transitioned(state_name)

## Path to the initial active state. 
@export var state: State:
	get:
		return state


func _ready() -> void:
	await owner.ready
	for child in get_children():
		child.fsm = self
	state.on_enter()


func _process(delta: float) -> void:
	state.update(delta)


func _physics_process(delta: float) -> void:
	state.physics_update(delta)


func transition_to(target_state_name: String, msg: Dictionary = {}) -> void:
	if not has_node(target_state_name):
		push_error("State " + target_state_name + " not exists.")
		return

	state.on_exit()
	state = get_node(target_state_name)
	state.on_enter(msg)
	emit_signal("transitioned", state.name)
