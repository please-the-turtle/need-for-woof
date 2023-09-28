extends Control


var label:
	set(value):
		$Label.text = value
	get:
		return $Label.text


func play() -> void:
	visible = true
	$AnimationPlayer.play("rotate")


func stop() -> void:
	visible = false
	$AnimationPlayer.stop()
