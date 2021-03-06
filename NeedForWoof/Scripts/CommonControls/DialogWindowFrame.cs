using Godot;

namespace NeedForWoof
{
    public class DialogWindowFrame : NinePatchRect
    {
        private AnimationPlayer _animationPlayer; 
    
        public override void _Ready()
        {
            base._Ready();

            _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
            _animationPlayer.CurrentAnimation = "open";
        }
    }
}
