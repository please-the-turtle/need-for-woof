class_name ChangeSceneButton
extends Button


## Absolute path to scene. 
## Example: res://scenes/level.tscn
@export_file("*.tscn") var scene_path: String


func _pressed() -> void:
	SceneChanger.go_to_scene(scene_path)