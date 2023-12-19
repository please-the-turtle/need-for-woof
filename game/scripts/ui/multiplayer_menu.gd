extends Control

signal connecting_done(error: Error)


@onready var dialog: OffsetContainer = $MessageDialog
@onready var loading: Control = $Loading
@onready var menu = $VBoxContainer
@onready var backButton = $BackButton

var thread: Thread = Thread.new()


func _ready():
	connecting_done.connect(_on_connecting_done)
	
	menu.visible = false
	loading.label = "connecting"
	loading.play()
	backButton.disabled = true
	
	thread.start(_try_to_connect_to_server)


func _try_to_connect_to_server():
	var address = GameSettings.DEFAULT_SERVER_ADDR
	if GameSettings.get_setting(GameSettings.USE_CUSTOM_ADDR):
		address = GameSettings.get_setting(GameSettings.CUSTOM_SERVER_ADDR)
	
	var error = ServerClient.connect_to_server(address)
	emit_signal.call_deferred("connecting_done", error)


func _on_connecting_done(error):
	loading.stop()
	
	if error != OK:
		if error == ERR_CONNECTION_ERROR or error == ERR_TIMEOUT:
			dialog.message = "The server is unavailable"
		dialog.open()
		await dialog.ok_pressed
		SceneChanger.go_to_scene("res://scenes/ui/main_menu.tscn")
		return
	
	menu.visible = true
	backButton.disabled = false


func _exit_tree():
	thread.wait_to_finish()


func _on_back_button_pressed():
	ServerClient.disconnect_from_server()


func _on_room_id_line_text_changed(new_text: String):
	var id_line = %RoomIdLine
	var cursor_position = id_line.caret_column
	id_line.text = new_text.to_upper()
	id_line.caret_column = cursor_position


func _on_create_button_pressed():
	var error = ServerClient.create_room()
	if error != OK:
		dialog.message = "Failed to create room"
		dialog.open()
		return
	
	SceneChanger.go_to_scene("res://scenes/ui/room_screen.tscn")


func _on_connect_button_pressed():
	var error = ServerClient.join_room(%RoomIdLine.text)
	if error != OK:
		dialog.message = "Failed join to the room"
		dialog.open()
		return
	
	SceneChanger.go_to_scene("res://scenes/ui/room_screen.tscn")
