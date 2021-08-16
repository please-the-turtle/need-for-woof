using Godot;

namespace NeedForWoof.Scripts
{
	/// <summary>
	/// <para>The base class of the player's person</para>
	/// </summary>
	public abstract class Dog : KinematicBody2D
	{
		/// <summary>
		/// <para>Constant speed of the Dog</para>
		/// </summary>
		[Export(PropertyHint.Range, "1, 1000, or_greater")]
		public float Speed = 200;
		
		/// <summary>
		/// <para>Angular rate of rotation. Measured in radians</para>
		/// </summary>
		[Export(PropertyHint.Range, "0, 3")] public float TurnSpeed = 1f;

		/// <summary>
		/// <para>BoostSpeed. Used for jumping. Controlled by the AnimationPlayer.</para>
		/// </summary>
		public float SpeedBoost = 1f;
		
		public MoveState MoveState = MoveState.Run;
		
		private Vector2 _velocity = Vector2.Zero;
		private Vector2 _forwardDirection = Vector2.Up;
		private AnimationPlayer _animationPlayer;
		
		public override void _Ready()
		{
			_animationPlayer = GetNode<AnimationPlayer>("Visualization/AnimationPlayer");
			_animationPlayer.CurrentAnimation = "run";
		}

		public override void _PhysicsProcess(float delta)
		{
			base._PhysicsProcess(delta);

			if (Input.IsActionPressed("ui_right"))
			{
				RunRight();
			}
			if (Input.IsActionPressed("ui_left"))
			{
				RunLeft();
			}

			if (Input.IsActionPressed("ui_up"))
			{
				Jump();
			}
			
			RunAhead();
			MoveAndSlide(_velocity);
			_velocity = Vector2.Zero;
		}

		public override void _Process(float delta)
		{
			base._Process(delta);
		}

		public void SetMoveState(MoveState moveState)
		{
			MoveState = moveState;
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
			direction *= Speed * SpeedBoost;
			_velocity += direction;
		}
		
		private void Turn(bool left = true)
		{
			if (MoveState != MoveState.Jump)
			{
				float phi = TurnSpeed * GetPhysicsProcessDeltaTime();
				if (left) phi = -phi;
				_forwardDirection = _forwardDirection.Rotated(phi);
				Rotate(phi);
			}
		}
		
	}

	public enum MoveState
	{
		Run = 0, 
		Jump = 1
	}
}
