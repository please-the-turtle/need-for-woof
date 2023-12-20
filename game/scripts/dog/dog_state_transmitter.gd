## DogStateTransmitter transmits information about changes 
## in the dog's state to the remote players.
class_name DogStateTransmitter
extends Node


@export var target: Dog

var _players_manager: PlayersManager
var _prev_position: Vector2 = Vector2.ZERO
var _prev_animation: String = "idle"


func _ready():
	if target == null:
		printerr("Target is null")
		return
	
	_players_manager = SharedStorage.take(SharedStorage.PLAYERS_MANAGER)
	if _players_manager == null:
		printerr("PlayersManager instance not found.")
		return
	
	await target.ready
	target.animation.current_animation_changed.connect(_on_animation_changed)


func _physics_process(_delta):
	if _prev_position != target.position:
		_prev_position = target.position
		_players_manager.send_local_player_position(target.position)


func _on_animation_changed(animation_name: String):
	if _prev_animation == animation_name:
		return
	
	_prev_animation = animation_name
	_players_manager.send_local_player_animation(animation_name)
