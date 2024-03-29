extends Area2D


signal dog_finished(dog: Dog)


func _on_body_exited(body: Node2D):
	if body is Dog:
		dog_finished.emit(body)
