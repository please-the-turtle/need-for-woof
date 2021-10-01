using Godot;

namespace NeedForWoof.Scripts.CommonControls
{
    public class DialogWindowFrame : NinePatchRect
    {
        private AnimationPlayer _animationPlayer; 
    
        public override void _Ready()
        {
            _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
            _animationPlayer.CurrentAnimation = "open";
        }
    }
}
