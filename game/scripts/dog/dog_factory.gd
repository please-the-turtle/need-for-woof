class_name DogFactory
extends Object


const _dogs_map: Dictionary = {
	Player.DogType.SHIBAINU: "res://scenes/level/dog/shibainu.tscn",
}

var _loaded_dog_packed_scenes: Dictionary = {}


## Creates Dog node for local player and set it movement controller and camera
func create_local_dog(local_player: Player) -> Dog:
	var dog = create_dog(local_player)
	dog.name = local_player.id
	dog.nickname = local_player.nickname
	
	var camera = load("res://scenes/level/player_camera.tscn").instantiate()
	dog.add_child(camera)
	
	var controller = load("res://scenes//level/dog/dog_movement_controller.tscn").instantiate()
	controller.target = dog
	dog.add_child(controller)
	
	var transmitter = load("res://scenes/level/dog/dog_state_transmitter.tscn").instantiate()
	transmitter.target = dog
	dog.add_child(transmitter)
	
	return dog


## Creates Dog node for remote player and set it remote controller
func create_remote_dog(remote_player: Player) -> Dog:
	var dog = create_dog(remote_player)
	dog.name = remote_player.id
	dog.nickname = remote_player.nickname
	
	return dog


func create_dog(player: Player) -> Dog:
	var dog_scene: PackedScene = _loaded_dog_packed_scenes.get(player.DogType)
	if dog_scene == null:
		var scene_path = _dogs_map.get(player.dog_type)
		var packed_scene = load(scene_path)
		if not packed_scene is PackedScene:
			printerr("PackedScene with players dog type not found.")
			return null
		dog_scene = packed_scene
		_loaded_dog_packed_scenes[player.dog_type] = packed_scene
	
	return dog_scene.instantiate()
