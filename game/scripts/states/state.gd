# Virtual base class for all states.
class_name State
extends Node


var fsm: StateMachine = null


func on_enter(_msg := {}) -> void:
	pass


func on_exit() -> void:
	pass


func update(_delta: float) -> void:
	pass


func physics_update(_delta: float) -> void:
	pass
