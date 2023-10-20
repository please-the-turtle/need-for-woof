class_name Player
extends Object


enum DogType {
	SIBA,
}


var id: String = ""
var nickname: String = "nickname"
var dog_type: DogType = DogType.SIBA
var dog: Dog = null
var ready: bool = false


func _init(_id: String = ""):
	id = _id
