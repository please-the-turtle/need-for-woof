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

		[Export(PropertyHint.Range, "0, 3.14")] public float TurnSpeed = .5f;

		public MoveState MoveState = MoveState.Run;
		
		private Vector2 _velocity = Vector2.Zero;
		private Vector2 _forwardDirection = Vector2.Up;
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
			
			RunAhead();
			MoveAndSlide(_velocity);
			_velocity = Vector2.Zero;
		}

		public override void _Process(float delta)
		{
			base._Process(delta);
		}
		
		public void RunAhead()
		{
			RunTo(_forwardDirection);
		}

		public void RunRight()
		{
			RunTo(Vector2.Right);
		}

		public void RunLeft()
		{
			RunTo(Vector2.Left);
		}

		public void TurnRight()
		{
			Turn(false);
		}

		public void TurnLeft()
		{
			Turn();
		}
		
		public void Jump()
		{
			_animationPlayer.CurrentAnimation = "jump";
			_animationPlayer.Queue("run");
		}
		
		private void RunTo(Vector2 direction)
		{
			direction = direction.Normalized();
			direction *= Speed;
			_velocity += direction;
		}
		
		private void Turn(bool left = true)
		{
			float phi = TurnSpeed * GetPhysicsProcessDeltaTime();
			if (left) phi = -phi;
			_forwardDirection = _forwardDirection.Rotated(phi);
			Rotate(phi);
		}
		
	}

	public enum MoveState
	{
		Run, 
		Jump
	}
}
