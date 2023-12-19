## OffsetContainer used to change the position of the control 
## relative to the originally set one. 
## This can be useful when animating the control position on the screen.
class_name OffsetContainer
extends Control


@export var position_offset: Vector2 = Vector2.ZERO:
	get: 
		return position_offset
	set(value):
		position -= position_offset
		position_offset = value
		position += position_offset
