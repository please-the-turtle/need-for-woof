extends Node


const SETTINGS_PATH = "user://settings.json"

const NICKNAME = "nickname"
const SOUNDS_VOLUME = "sounds_volume"
const MUSIC_VOLUME = "music_volume"
const USE_CUSTOM_ADDR = "use_custom_address"
const CUSTOM_SERVER_ADDR = "custom_server_address"

const DEFAULT_SETTINGS = {
	NICKNAME: "go0d boy",
	SOUNDS_VOLUME: 50.0,
	MUSIC_VOLUME: 50.0,
	USE_CUSTOM_ADDR: false,
	CUSTOM_SERVER_ADDR: "localhost:5553",
}

var _settings_dict: Dictionary

func _ready():
	_settings_dict = _load_game_settings()


## Sets new value to the game setting. 
func set_setting(key: String, value: Variant) -> void:
	_settings_dict[key] = value


## Gets value from the game settings. 
## Returns null if setting not found.
func get_setting(key) -> Variant:
	return _settings_dict.get(key)


func save_game_settings() -> void:
	call_deferred("_save_game_settings_deferred")


func _load_game_settings() -> Dictionary:
	var settings = DataManager.load_data(SETTINGS_PATH)
	if settings == null:
		settings = DEFAULT_SETTINGS.duplicate(true)
		push_warning("GameSettings: Loading game settings failed. Default settings applied.")
	return settings


func _save_game_settings_deferred() -> void:
	var success = DataManager.save_data(SETTINGS_PATH, _settings_dict)
	if !success:
		printerr("GameSettings: Opening settings file failed")
