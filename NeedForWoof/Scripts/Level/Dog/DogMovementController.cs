using Godot;

namespace NeedForWoof.Level
{
    public class DogMovementController : Node
    {
        // TODO чисти чисти
        private DogMovement _dogMovement;
        private Dog _dog;

        public DogMovementController(DogMovement movement)
        {
            _dogMovement = movement;
        }

        public override void _Ready()
        {
            base._Ready();

            _dog = _dogMovement.GetParent<Dog>();
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);

            UpdateDogMovement();
        }

        private void UpdateDogMovement()
        {
            if (Input.IsActionPressed("ui_right"))
            {
                _dogMovement.RunRight();
                _dogMovement.RpcUnreliable(nameof(_dogMovement.UpdateRemotePosition), _dog.Position);
            }

            if (Input.IsActionPressed("ui_left"))
            {
                _dogMovement.RunLeft();
                _dogMovement.RpcUnreliable(nameof(_dogMovement.UpdateRemotePosition), _dog.Position);
            }
            
            if (Input.IsActionPressed("ui_up"))
            {
                _dogMovement.Rpc(nameof(_dogMovement.Jump));
            }
        }
    }
}