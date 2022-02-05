using Godot;
using System;

namespace NeedForWoof.Level
{
    public class DogCreator : Node
    {
        public static Dog CreateDog(DogType dog)
        {
            Dog newDog;

            switch (dog)
            {
                case DogType.Siba:
                    PackedScene dogScene = (PackedScene)GD.Load("res://Scenes/Level/SibaDog.tscn");
                    newDog = dogScene.Instance<Dog>();
                    break;
                default:
                    throw new NotImplementedException("This dog type not inmplemented: " + nameof(dog));
            }

            return newDog;
        }    
    }
}