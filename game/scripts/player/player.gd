class_name Player
extends Object


enum DogType {
	SHIBAINU,
}


var id: String = ""
var nickname: String = "nickname"
var dog_type: DogType = DogType.SHIBAINU
var dog: Dog = null
var ready: bool = false


func _init(_id: String = ""):
	id = _id
