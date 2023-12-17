extends Node2D


var _players_manager: PlayersManager
var _local_dog: Dog
var _dogs: Array = []
var _dog_factory: DogFactory = DogFactory.new()


func _ready():
	_players_manager = SharedStorage.take(SharedStorage.PLAYERS_MANAGER)
	if _players_manager == null:
		printerr("PlayersManager not found in SharedStorage.")
	
	if _players_manager.is_all_players_ready():
		%Countdown.start()
	_players_manager.player_ready_changed.connect(_on_player_ready_changed)
	
	_spawn_dogs()
	
	_players_manager.set_local_player_ready_for_all(true)


func _on_player_ready_changed(_player: Player):
	if _players_manager.is_all_players_ready():
		%Countdown.start()


func _spawn_dogs():
	_local_dog = _dog_factory.create_local_dog(_players_manager.local_player)
	$LevelGUI.target = _local_dog
	_dogs.append(_local_dog)
	
	for player in _players_manager.remote_players.values():
		var remote_dog = _dog_factory.create_remote_dog(player)
		_dogs.append(remote_dog)
	
	_dogs.sort_custom(func(a, b):
		return String(a.name) > String(b.name)
	)
	
	var start_line = $StartLine
	var dogs_number = _dogs.size()
	for i in dogs_number:
		var dog: Dog = _dogs[i]
		var dog_pos = Vector2(
			start_line.rect_width / (dogs_number + 1) * (i + 1) + start_line.first_point.x,
			start_line.rect_height / (dogs_number + 1) * (i + 1) + start_line.first_point.y
		)
		dog.position = dog_pos
		add_child(dog)


func _on_countdown_is_over():
	_local_dog.fsm.transition_to("DogStateRun")


func _on_finish_line_dog_finished(_dog):
	_players_manager.set_local_player_ready_for_all(false)


func _on_level_gui_home_button_pressed():
	SharedStorage.delete(SharedStorage.PLAYERS_MANAGER)
	_players_manager.free()
	ServerClient.leave_room()
