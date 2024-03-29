## State for remote dog controlling
class_name DogStateRemote
extends DogState


var _players_manager: PlayersManager


func _ready():
	_players_manager = SharedStorage.take(SharedStorage.PLAYERS_MANAGER)


func on_enter(_msg := {}) -> void:
	target.animation.play("idle")
	if _players_manager == null:
		printerr("PlayersManager not found.")
		return
	
	_players_manager.player_position_changed.connect(_on_player_position_changed)
	_players_manager.player_animation_changed.connect(_on_player_animation_changed)


func on_exit() -> void:
	if _players_manager == null:
		return
	
	_players_manager.player_position_changed.disconnect(_on_player_position_changed)
	_players_manager.player_animation_changed.disconnect(_on_player_animation_changed)


func push(impulse: Vector2) -> void:
	_players_manager.send_push_dog(target, impulse)


func _on_player_position_changed(player_id: String, new_position: Vector2):
	if player_id != target.name:
		return
	
	target.position = new_position


func _on_player_animation_changed(player_id: String, animation_name: String):
	if player_id != target.name:
		return
	
	target.animation.play(animation_name)
