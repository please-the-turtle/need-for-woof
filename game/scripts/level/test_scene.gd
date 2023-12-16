extends Node2D


func _ready():
	$FinishLine.dog_finished.connect(_put_dog_on_start)
	%Countdown.is_over.connect(func():
		$Siba.fsm.transition_to("DogStateRun")
	)
	%Countdown.start()


func _put_dog_on_start(dog: Dog):
	#dog.position = Vector2(241, 413)
	dog.fsm.transition_to("DogStateIdle")


func _physics_process(_delta):
	if Input.is_key_pressed(KEY_R):
		$Siba.position = Vector2(241, 413)
		$Siba.fsm.transition_to("DogStateRun")
		$Siba2.position = Vector2(381, -314)
		$Siba2.fsm.transition_to("DogStateIdle")
