class_name BackButton
extends Button


func _notification(what):
	if what == NOTIFICATION_WM_GO_BACK_REQUEST:
		SceneChanger.go_to_previous_scene()


func _pressed() -> void:
	SceneChanger.go_to_previous_scene()
