using Godot;
using NeedForWoof.Scripts;

namespace NeedForWoof.Scenes
{
	public class PlayerScreenController : CanvasLayer
	{
		[Export(PropertyHint.ResourceType, "KinematicBody2D")]
		public NodePath Player;
	
		private Dog _player;
	
		private TextureButton _runRightButton; 
		private TextureButton _runLeftButton; 
		private TextureButton _jumpButton; 
	
		public override void _Ready()
		{
			_player = GetNode<Dog>(Player);
			if (_player == null)
			{
				GD.PrintErr("Player is not defined");
			}

			_runRightButton = GetNode<TextureButton>("ControllerButtons/RunRightButton");
			_runLeftButton = GetNode<TextureButton>("ControllerButtons/RunLeftButton");
			_jumpButton = GetNode<TextureButton>("ControllerButtons/JumpButton");
		}

		public override void _PhysicsProcess(float delta)
		{
			base._PhysicsProcess(delta);

			if (_runRightButton.Pressed)
			{
				_player.TurnRight();
			}
			if (_runLeftButton.Pressed)
			{
				_player.TurnLeft();
			}
		}

		public void OnJumpButton_pressed()
		{
			_player.Jump();
		}
	}
}
