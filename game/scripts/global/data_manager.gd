class_name DataManager
extends Node


static func load_data(path: String) -> Variant:
	var json = FileAccess.get_file_as_string(path)
	return JSON.parse_string(json)


static func save_data(save_path: String, data: Dictionary) -> bool:
	var json = JSON.stringify(data)
	var file = FileAccess.open(save_path, FileAccess.WRITE)
	if file == null:
		return false
	file.store_string(json)
	file.close()
	return true
