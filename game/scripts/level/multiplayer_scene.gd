extends Node2D


var _players_manager: PlayersManager
var _local_dog: Dog
var _dogs: Dictionary = {}
var _dog_factory: DogFactory = DogFactory.new()

@onready var _gui: LevelGUI = $LevelGUI


func _ready():
	_players_manager = SharedStorage.take(SharedStorage.PLAYERS_MANAGER)
	if _players_manager == null:
		printerr("PlayersManager not found in SharedStorage.")
		return
	
	_players_manager.player_status_changed.connect(_on_player_status_changed)
	_players_manager.player_left.connect(_on_player_left)
	
	_players_manager.set_local_player_status_for_all(PlayerStatus.ON_LEVEL_READY)


func _on_player_status_changed(_player: Player):
	if not _is_all_players_ready():
		return
	
	_put_dogs_on_start()
	_players_manager.set_local_player_status_for_all(PlayerStatus.ON_LEVEL_ACTIVE)
	_gui.show_dog_controls()
	%Countdown.start()


func _is_all_players_ready() -> bool:
	if _players_manager.local_player.status != PlayerStatus.ON_LEVEL_READY:
		return false
	
	for player in _players_manager.remote_players.values():
		if player.status != PlayerStatus.ON_LEVEL_READY:
			return false
	
	return true


func _put_dogs_on_start():
	_local_dog = _dogs.get(_players_manager.local_player.id)
	if _local_dog == null:
		_local_dog = _dog_factory.create_local_dog(_players_manager.local_player)
		$LevelGUI.target = _local_dog
		_dogs[_players_manager.local_player.id] = _local_dog
		add_child(_local_dog)
	
	for player in _players_manager.remote_players.values():
		var remote_dog = _dogs.get(player.id)
		if remote_dog == null:
			remote_dog = _dog_factory.create_remote_dog(player)
			_dogs[player.id] = remote_dog
			add_child(remote_dog)
			remote_dog.fsm.transition_to("DogStateRemote")
	
	_set_dogs_start_position()


func _dogs_sorter(a: Dog, b: Dog) -> bool:
	return String(a.name) > String(b.name)


func _set_dogs_start_position():
	var start_line = $StartLine
	var dogs_count = _dogs.size()
	var sorted_dogs = _dogs.values()
	sorted_dogs.sort_custom(_dogs_sorter)
	for i in dogs_count:
		var dog_pos = lerp(
			start_line.first_point, 
			start_line.last_point, 
			(i + 1.0)/(dogs_count + 1.0)
		)
		sorted_dogs[i].position = dog_pos


func _on_countdown_is_over():
	_local_dog.fsm.transition_to("DogStateRun")


func _on_finish_line_dog_finished(dog):
	if dog == _local_dog:
		dog.fsm.transition_to("DogStateIdle")
		_players_manager.set_local_player_status_for_all(PlayerStatus.ON_LEVEL_NOT_READY)
		_gui.show_finished_buttons()


func _on_player_left(player: Player):
	var dog = find_child(player.id, false, false)
	if dog == null:
		return
	
	_dogs.erase(player.id)
	remove_child(dog)
	dog.queue_free()


func _on_level_gui_home_button_pressed():
	process_mode = Node.PROCESS_MODE_DISABLED
	
	_players_manager.send_local_player_returned_to_room()
	SharedStorage.delete(SharedStorage.PLAYERS_MANAGER)
	_players_manager.free()
	
	SceneChanger.go_to_scene("res://scenes/ui/room_screen.tscn")


func _on_level_gui_restart_button_toggled(toggled_on):
	var status = PlayerStatus.ON_LEVEL_READY if toggled_on \
			else PlayerStatus.ON_LEVEL_NOT_READY
	_players_manager.set_local_player_status_for_all(status)
