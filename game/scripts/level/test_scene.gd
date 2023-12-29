extends Node2D


func _ready():
	$FinishLine.dog_finished.connect(func(dog):
		dog.fsm.transition_to("DogStateIdle")
		
		if dog == $Shiba:
			$LevelGUI.show_finished_buttons()
	)
	%Countdown.is_over.connect(func():
		$Shiba.fsm.transition_to("DogStateRun")
	)
	$LevelGUI.restart_button_toggled.connect(_on_restart_button_toggled)
	%Countdown.start()


func _physics_process(_delta):
	if Input.is_key_pressed(KEY_R):
		_reset_level()


func _on_restart_button_toggled(toggled: bool):
	if not toggled:
		return
	
	$LevelGUI.show_dog_controls()
	_reset_level()


func _reset_level():
	$Shiba.position = Vector2(241, 413)
	$Shiba.fsm.transition_to("DogStateRun")
	$Shiba.stamina = $Shiba.max_stamina
	$Shiba2.position = Vector2(381, -314)
	$Shiba2.fsm.transition_to("DogStateRun")


func _on_level_gui_home_button_pressed():
	SceneChanger.go_to_scene("res://scenes/ui/main_menu.tscn")
