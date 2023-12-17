extends Line2D


var first_point: Vector2:
	get:
		return position + points[0]

var last_point: Vector2:
	get:
		return position + points[1]

var rect_width: float:
	get:
		return last_point.x - first_point.x

var rect_height: float:
	get:
		return last_point.y - first_point.y

var length: float:
	get:
		return first_point.distance_to(last_point)
