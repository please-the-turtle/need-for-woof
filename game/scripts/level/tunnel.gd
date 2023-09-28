extends StaticBody2D


func _on_bonus_zone_body_entered(body: Node2D):
	if body is Dog:
		body.fsm.transition_to("DogStateTunnel")


func _on_bonus_zone_body_exited(body: Node2D):
	if body is Dog:
		body.fsm.transition_to("DogStateRun")
