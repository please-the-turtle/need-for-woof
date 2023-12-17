class_name DogMovementController
extends Node


@export var target: Dog


func _physics_process(_delta):
	if Input.is_action_pressed("ui_left"):
		target.run_left()
	
	if Input.is_action_pressed("ui_right"):
		target.run_right()
		
	if Input.is_action_pressed("ui_jump"):
		target.jump()
