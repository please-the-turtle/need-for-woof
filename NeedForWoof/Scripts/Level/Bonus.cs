using Godot;

namespace NeedForWoof.Level
{
    public class Bonus : Area2D
    {
        [Export(PropertyHint.Range, "1, 10, or_greater")]
        public int BonusSize = 1;

        private AnimationPlayer _animationPlayer;

        public override void _Ready()
        {
            GetNode<Sprite>("Sprite").Scale = new Vector2(3.5f, 3.5f);
            _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
            _animationPlayer.CurrentAnimation = "idle";
        }

        public void Taken()
        {
            _animationPlayer.CurrentAnimation = "taken";
        }

        public void OnCookie_body_entered(Node body)
        {
            if (body is Dog dog)
            {
                dog.Score += BonusSize;
                Taken();
            }
        }
    }
}
