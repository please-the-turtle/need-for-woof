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
	%Countdown.start()


func _physics_process(_delta):
	if Input.is_key_pressed(KEY_R):
		$Shiba.position = Vector2(241, 413)
		$Shiba.fsm.transition_to("DogStateRun")
		$Shiba2.position = Vector2(381, -314)
		$Shiba2.fsm.transition_to("DogStateIdle")
