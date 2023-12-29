class_name Player
extends Object


var id: String = ""
var nickname: String = "nickname"
var dog_type: int = DogType.SHIBAINU
var dog: Dog = null
var status: int = PlayerStatus.NOT_IN_THE_ROOM


func _init(_id: String = ""):
	id = _id
