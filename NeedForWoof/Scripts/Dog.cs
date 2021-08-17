using Godot;

namespace NeedForWoof.Scripts
{
	/// <summary>
	/// <para>The base class of the player's person</para>
	/// </summary>
	public abstract class Dog : KinematicBody2D
	{
		[Signal]
		public delegate void Finished();
		
		public MoveState MoveState = MoveState.Run;
		
		private AnimationPlayer _animationPlayer;
		private DogMovement _movement;

		public override void _Ready()
		{
			_animationPlayer = GetNode<AnimationPlayer>("Visualization/AnimationPlayer");
			_animationPlayer.CurrentAnimation = "run";
			_movement = GetNode<DogMovement>("DogMovement");
			_movement.Go();
		}

		public void SetMoveState(MoveState moveState)
		{
			MoveState = moveState;
		}

		public void PlayAnimation(string animation)
		{
			_animationPlayer.CurrentAnimation = animation;
			_animationPlayer.Queue("run");
		}

		public void Finish(Node body)
		{
			MoveState = MoveState.Finished;
			_animationPlayer.CurrentAnimation = "finish";
			_movement.Stop();
			EmitSignal(nameof(Finished));
			GD.Print("finish");
		}
		
	}

	public enum MoveState
	{
		Run = 0, 
		Jump = 1,
		Finished = 2
	}
}
