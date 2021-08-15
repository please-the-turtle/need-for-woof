using Godot;

namespace NeedForWoof.Scripts
{
	/// <summary>
	/// <para>The base class of the player's person</para>
	/// </summary>
	public abstract class Dog : KinematicBody2D
	{
		[Export(PropertyHint.Range, "1, 1000, or_greater")]
		public float Speed = 200;
		
		private Vector2 _velocity = Vector2.Zero;
		private AnimatedSprite _animatedSprite;
		private AnimationPlayer _animationPlayer;
		
		public override void _Ready()
		{
			_animatedSprite = GetNode<AnimatedSprite>("Visualization/AnimatedSprite");
			_animationPlayer = GetNode<AnimationPlayer>("Visualization/AnimationPlayer");
			_animationPlayer.CurrentAnimation = "run";
		}

		public override void _PhysicsProcess(float delta)
		{
			base._PhysicsProcess(delta);
			
			MoveAndCollide(_velocity);
			_velocity = Vector2.Zero;
		}

		public override void _Process(float delta)
		{
			base._Process(delta);
		}
		
		public void RunTo(Vector2 direction)
		{
			direction = direction.Normalized();
			direction *= Speed * GetPhysicsProcessDeltaTime();
			_velocity = direction;
		}

		public void RunRight()
		{
			RunTo(Vector2.Right);
		}

		public void RunLeft()
		{
			RunTo(Vector2.Left);
		}

		public void Jump()
		{
			_animationPlayer.CurrentAnimation = "jump";
			_animationPlayer.Queue("run");
		}
		
	}
}
