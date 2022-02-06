using System.Collections.Generic;
using Godot;

namespace NeedForWoof.Level
{
    public class DogMovementController : Node
    {
        // TODO чисти чисти
        private DogMovement _dogMovement;
        private Dog _dog;

        private bool _leftPressed = false;
        private bool _rightPressed = false;
        private bool _upPressed = false;

        public override void _Ready()
        {
            base._Ready();

            _dog = GetParent<Dog>();
            _dogMovement = _dog.GetNode<DogMovement>("DogMovement");
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);

            CheckInputActions();
            UpdateDogMovement();
        }

        [RemoteSync]
        public void SetRightPressed(bool rightPressed)
        {
            _rightPressed = rightPressed;
        }

        [RemoteSync]
        public void SetLeftPressed(bool leftPressed)
        {
            _leftPressed = leftPressed;
        }

        [RemoteSync]
        public void SetUpPressed(bool upPressed)
        {
            _upPressed = upPressed;
        }

        private void CheckInputActions()
        {
            bool pressed;

            if (_rightPressed != Input.IsActionPressed("ui_right"))
            {
                pressed = Input.IsActionPressed("ui_right");
                _dog.GetParent<DogsManager>().GetDogById(GetTree().GetNetworkUniqueId()).GetNode<DogMovementController>("MovementController").Rpc(nameof(SetRightPressed), pressed);
            }

            if (_leftPressed != Input.IsActionPressed("ui_left"))
            {
                pressed = Input.IsActionPressed("ui_left");
                _dog.GetParent<DogsManager>().GetDogById(GetTree().GetNetworkUniqueId()).GetNode<DogMovementController>("MovementController").Rpc(nameof(SetLeftPressed), pressed);
            }

            if (_upPressed != Input.IsActionPressed("ui_up"))
            {
                pressed = Input.IsActionPressed("ui_up");
                _dog.GetParent<DogsManager>().GetDogById(GetTree().GetNetworkUniqueId()).GetNode<DogMovement>("DogMovement").Rpc(nameof(DogMovement.Jump));
            }
        }

        private void UpdateDogMovement()
        {
            if (_rightPressed)
            {
                _dogMovement.RunRight();
            }

            if (_leftPressed)
            {
                _dogMovement.RunLeft();
            }
            
            if (_upPressed)
            {
                _dogMovement.Jump();
            }
        }
    }
}