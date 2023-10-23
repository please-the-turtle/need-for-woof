extends Node


var current_scene: Node:
	get:
		return _current_scene


var previous_scene_file: String = "":
	get:
		return _previous_scene_file


var _current_scene: Node
var _previous_scene_file: String


func _ready():
	var root = get_tree().root
	_current_scene = root.get_child(root.get_child_count() - 1)
	previous_scene_file = current_scene.scene_file_path


## Transitions to another scene. 
## The path argument must contain the absolute path to the packed scene file.
## Example: res://scenes/level.tscn
func go_to_scene(path: String) -> void:
	call_deferred("_go_to_scene_deferred", path)


func go_to_previous_scene() -> void:
	if !previous_scene_file.is_absolute_path():
		return
	
	go_to_scene(previous_scene_file)


func _go_to_scene_deferred(path: String) -> void:
	if previous_scene_file != current_scene.scene_file_path:
		_previous_scene_file = current_scene.scene_file_path
		
	_current_scene.free()
	var next_scene = load(path)
	if next_scene == null:
		printerr("Scene changer: Loading scene ", path, " failed")
		return
		
	_current_scene = next_scene.instantiate()
	get_tree().root.add_child(_current_scene)
