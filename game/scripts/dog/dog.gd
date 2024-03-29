class_name Dog
extends CharacterBody2D


@export_range(1, 2000, 100, "or_greater") var speed: float = 600.0
@export_range(0, 1000, 10, "or_greater") var max_stamina: float = 500.0
@export_range(0, 1000, 10, "or_greater") var stamina_refill: float = 25.0

## The cost of stamina units during the jump per second.
@export_range(0, 500, 10, "or_greater") var jump_stamina_expenditure: float = 100.0

## The value by which the speed is multiplied when jumping.
@export_range(1, 10, 0.5, "or_greater") var jumping_speed_boost: float = 1.5


@onready var animation: AnimationPlayer = $AnimationPlayer
@onready var fsm: DogStateMachine = $DogStateMachine
@onready var ray_cast: RayCast2D = %RayCast2D


## Multiplies the speed of movement in various situations. 
## For example, when jumping.
var speed_multiplier: float = 1

var stamina: float:
	get:
		return stamina
	set(value):
		if value > max_stamina:
			stamina = max_stamina
		elif value < 0:
			stamina = 0
		else:
			stamina = value

var nickname: String:
	get:
		return %NicknameLabel.text
	set(value):
		%NicknameLabel.text = value


func _ready():
	stamina = max_stamina


func _physics_process(delta):
	stamina += stamina_refill * delta


func jump() -> void:
	fsm.jump()


func run_left() -> void:
	fsm.run_left()


func run_right() -> void:
	fsm.run_right()


func push(impulse: Vector2) -> void:
	fsm.push(impulse)
