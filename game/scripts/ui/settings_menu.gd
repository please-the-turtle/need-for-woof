extends Control


var game_settings: GameSettings


func _ready():
	game_settings = GameSettings
	
	%Nickname.text = game_settings.get_setting(game_settings.NICKNAME)
	%MusicSlider.value = game_settings.get_setting(game_settings.MUSIC_VOLUME)
	%SoundsSlider.value = game_settings.get_setting(game_settings.SOUNDS_VOLUME)
	var use_custom_addr = game_settings.get_setting(game_settings.USE_CUSTOM_ADDR)
	%CustomAddressButton.button_pressed = use_custom_addr
	var addr_line = %CustomAddressLine
	addr_line.editable = use_custom_addr
	addr_line.focus_mode = FOCUS_ALL if use_custom_addr else FOCUS_NONE


func _on_nickname_text_changed(new_text):
	game_settings.set_setting(game_settings.NICKNAME, new_text)


func _on_music_slider_drag_ended(value_changed):
	if not value_changed:
		return
	
	game_settings.set_setting(game_settings.MUSIC_VOLUME, %MusicSlider.value)


func _on_sounds_slider_drag_ended(value_changed):
	if not value_changed:
		return
	
	game_settings.set_setting(game_settings.SOUNDS_VOLUME, %SoundsSlider.value)


func _on_custom_address_button_toggled(button_pressed):
	var addr_line = %CustomAddressLine
	addr_line.editable = button_pressed
	addr_line.focus_mode = FOCUS_ALL if button_pressed else FOCUS_NONE
	game_settings.set_setting(game_settings.USE_CUSTOM_ADDR, button_pressed)


func _on_custom_address_line_text_changed(new_text):
	game_settings.set_setting(game_settings.CUSTOM_SERVER_ADDR, new_text)


func _on_back_button_pressed():
	GameSettings.save_game_settings()
