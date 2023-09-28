extends Area2D


func _on_body_exited(body: Node2D):
	if body is Dog:
#		body.fsm.transition_to("DogStateIdle")
		body.position = Vector2(241, 413)
