extends Control


func _on_create_button_pressed():
	var scene_changer = get_node("/root/SceneChanger")
	scene_changer.go_to_scene("res://scenes/level/test_scene.tscn")
