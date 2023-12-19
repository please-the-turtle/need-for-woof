extends OffsetContainer


func _ready():
	visible = false


func _on_visibility_changed():
	if not visible:
		return
	
	$AnimationPlayer.play("pop_up")
	await  get_tree().create_timer(3.0).timeout
	visible = false
