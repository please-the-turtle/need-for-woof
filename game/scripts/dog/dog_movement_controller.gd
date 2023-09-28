class_name DogMovementController
extends Node


@export var _target: Dog


func _physics_process(_delta):
	if Input.is_action_pressed("ui_left"):
		_target.run_left()
	
	if Input.is_action_pressed("ui_right"):
		_target.run_right()
		
	if Input.is_action_pressed("ui_jump"):
		_target.jump()
