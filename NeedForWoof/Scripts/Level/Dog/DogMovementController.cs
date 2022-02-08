using System.Collections.Generic;
using Godot;

namespace NeedForWoof.Level
{
    public class DogMovementController : Node
    {
        // TODO чисти чисти
        private DogMovement _dogMovement;

        private bool _leftPressed = false;
        private bool _rightPressed = false;
        private bool _upPressed = false;

        public DogMovementController(DogMovement movement)
        {
            _dogMovement = movement;
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);

            if (Input.IsActionPressed("ui_right"))
            {
                _dogMovement.RunRight();
            }

            if (Input.IsActionPressed("ui_left"))
            {
                _dogMovement.RunLeft();
            }

            if (Input.IsActionPressed("ui_up"))
            {
                _dogMovement.Rpc(nameof(_dogMovement.Jump));
            }
        }
    }
}