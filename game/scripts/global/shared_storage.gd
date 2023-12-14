## Shared storage is used to access shared data from different scenes.
## It can be useful for transferring objects from one scene to another.

extends Node


const PLAYERS_MANAGER = "PlayersManager"


var _storage: Dictionary = {}


func take(key: String) -> Variant:
	return _storage.get(key)


func put(key: String, value):
	_storage[key] = value


func delete(key: String):
	_storage.erase(key)
