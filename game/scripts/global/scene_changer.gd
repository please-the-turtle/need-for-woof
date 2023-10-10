extends Node


var current_scene: Node:
	get:
		return _current_scene

var _current_scene: Node


func _ready():
	var root = get_tree().root
	_current_scene = root.get_child(root.get_child_count() - 1)


func go_to_scene(path: String) -> void:
	call_deferred("_go_to_scene_deferred", path)


func _go_to_scene_deferred(path: String) -> void:
	_current_scene.free()
	var next_scene = load(path)
	if next_scene == null:
		printerr("Scene changer: Loading scene ", path, " failed")
		return
	_current_scene = next_scene.instantiate()
	get_tree().root.add_child(_current_scene)
