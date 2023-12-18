extends TextureRect


func _ready():
	visible = false


## Used for position animation
@export var position_offset: Vector2 = Vector2.ZERO:
	get: 
		return position_offset
	set(value):
		position -= position_offset
		position_offset = value
		position += position_offset


func _on_visibility_changed():
	if not visible:
		return
	
	$AnimationPlayer.play("pop_up")
	await  get_tree().create_timer(3.0).timeout
	visible = false
