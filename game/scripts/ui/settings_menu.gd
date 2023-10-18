extends Control


func _ready():
	%Nickname.text = GameSettings.get_setting(GameSettings.NICKNAME)
	%MusicSlider.value = GameSettings.get_setting(GameSettings.MUSIC_VOLUME)
	%SoundsSlider.value = GameSettings.get_setting(GameSettings.SOUNDS_VOLUME)
	var use_custom_addr = GameSettings.get_setting(GameSettings.USE_CUSTOM_ADDR)
	%CustomAddressButton.button_pressed = use_custom_addr
	var addr_line = %CustomAddressLine
	addr_line.text = GameSettings.get_setting(GameSettings.CUSTOM_SERVER_ADDR)
	addr_line.editable = use_custom_addr
	addr_line.focus_mode = FOCUS_ALL if use_custom_addr else FOCUS_NONE


func _on_nickname_text_changed(new_text):
	GameSettings.set_setting(GameSettings.NICKNAME, new_text)


func _on_music_slider_drag_ended(value_changed):
	if not value_changed:
		return
	
	GameSettings.set_setting(GameSettings.MUSIC_VOLUME, %MusicSlider.value)


func _on_sounds_slider_drag_ended(value_changed):
	if not value_changed:
		return
	
	GameSettings.set_setting(GameSettings.SOUNDS_VOLUME, %SoundsSlider.value)


func _on_custom_address_button_toggled(button_pressed):
	var addr_line = %CustomAddressLine
	addr_line.editable = button_pressed
	addr_line.focus_mode = FOCUS_ALL if button_pressed else FOCUS_NONE
	GameSettings.set_setting(GameSettings.USE_CUSTOM_ADDR, button_pressed)


func _on_custom_address_line_text_changed(new_text):
	GameSettings.set_setting(GameSettings.CUSTOM_SERVER_ADDR, new_text)


func _on_back_button_pressed():
	GameSettings.save_game_settings()
