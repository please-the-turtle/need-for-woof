extends StaticBody2D


## Multiplies the speed of the dog that is in the tunnel.
@export_range(1, 10, 0.5, "or_greater") var speed_bonus: float = 2.0


func _on_bonus_zone_body_entered(body: Node2D):
	if body is Dog:
		print("INSIDE")
		body.speed_multiplier *= speed_bonus
		body.can_jump = false
		body.can_run_side = false


func _on_bonus_zone_body_exited(body: Node2D):
	if body is Dog:
		body.speed_multiplier /= speed_bonus
		body.can_jump = true
		body.can_run_side = true
