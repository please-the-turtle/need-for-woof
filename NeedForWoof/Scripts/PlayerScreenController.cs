using Godot;
using NeedForWoof.Scripts;

namespace NeedForWoof.Scenes
{
	public class PlayerScreenController : CanvasLayer
	{
		[Export(PropertyHint.ResourceType, "KinematicBody2D")]
		public NodePath Player;
	
		private Dog _player;
	
		private TouchScreenButton _runRightButton; 
		private TouchScreenButton _runLeftButton;

		public override void _Ready()
		{
			DefinePlayer();
			_runRightButton = GetNode<TouchScreenButton>("RunRightButton");
			_runLeftButton = GetNode<TouchScreenButton>("RunLeftButton");
		}

		public override void _PhysicsProcess(float delta)
		{
			base._PhysicsProcess(delta);

			if (_runRightButton.IsPressed())
			{
				_player.TurnRight();
			}
			if (_runLeftButton.IsPressed())
			{
				_player.TurnLeft();
			}
		}

		public void OnJumpButton_pressed()
		{
			_player.Jump();
		}

		private void DefinePlayer()
		{
			if (Player == null && GetParent().GetType() == typeof(Dog))
			{
				_player = GetParent<Dog>();
			}
			else _player = GetNode<Dog>(Player);
			if (_player == null)
			{
				GD.PrintErr("Player is not defined");
			}
		}
	}
}
