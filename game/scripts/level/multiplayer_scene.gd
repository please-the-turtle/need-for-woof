extends Node2D


var _players_manager: PlayersManager
var _local_dog: Dog
var _dogs: Array = []
var _dog_factory: DogFactory = DogFactory.new()

@onready var _gui: LevelGUI = $LevelGUI

func _ready():
	_players_manager = SharedStorage.take(SharedStorage.PLAYERS_MANAGER)
	if _players_manager == null:
		printerr("PlayersManager not found in SharedStorage.")
	
	_players_manager.player_ready_changed.connect(_on_player_ready_changed)
	_players_manager.player_left.connect(_on_player_left)
	
	_spawn_dogs()
	
	_players_manager.set_local_player_ready_for_all(true)


func _on_player_ready_changed(_player: Player):
	if _players_manager.is_all_players_ready():
		_put_dogs_on_start()
		_players_manager.set_local_player_ready_for_all(false)
		_gui.show_dog_controls()
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
	_put_dogs_on_start()
	
	for dog in _dogs:
		add_child(dog)
		if dog != _local_dog:
			dog.fsm.transition_to("DogStateRemote")


func _put_dogs_on_start():
	var start_line = $StartLine
	var dogs_number = _dogs.size()
	for i in dogs_number:
		var dog: Dog = _dogs[i]
		var dog_pos = lerp(
			start_line.first_point, 
			start_line.last_point, 
			(i + 1.0)/(dogs_number + 1.0)
		)
		dog.position = dog_pos


func _on_countdown_is_over():
	_local_dog.fsm.transition_to("DogStateRun")


func _on_finish_line_dog_finished(dog):
	if dog == _local_dog:
		dog.fsm.transition_to("DogStateIdle")
		_players_manager.set_local_player_ready_for_all(false)
		_gui.show_finished_buttons()


func _on_player_left(player: Player):
	var dog = find_child(player.id, false, false)
	if dog == null:
		return
	
	remove_child(dog)
	dog.queue_free()


func _on_level_gui_home_button_pressed():
	process_mode = Node.PROCESS_MODE_DISABLED
	
	SharedStorage.delete(SharedStorage.PLAYERS_MANAGER)
	_players_manager.free()
	ServerClient.leave_room()


func _on_level_gui_restart_button_toggled(toggled_on):
	_players_manager.set_local_player_ready_for_all(toggled_on)
