using Godot;

namespace NeedForWoof.Scripts
{
    public class DogMovement : Node
    {
        /// <summary>
        /// <para>Constant speed of the Dog</para>
        /// </summary>
        [Export(PropertyHint.Range, "1, 1000, or_greater")]
        public float Speed = 400;
		
        /// <summary>
        /// <para>Angular rate of rotation. Measured in radians</para>
        /// </summary>
        [Export(PropertyHint.Range, "0, 3")] public float TurnSpeed = 1f;

        /// <summary>
        /// <para>BoostSpeed. Used for jumping. Controlled by the AnimationPlayer.</para>
        /// </summary>
        public float SpeedBoost = 1f;
        
        private Dog _dog;
        
        private Vector2 _velocity = Vector2.Zero;
        private Vector2 _forwardDirection = Vector2.Up;
    
        public override void _Ready()
        {
            _dog = GetParent<Dog>();
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
            _dog.MoveAndSlide(_velocity);
            _velocity = Vector2.Zero;
        }

        public void Stop()
        {
            SetPhysicsProcess(false);
            SetProcess(false);
            _velocity = Vector2.Zero;
        }

        public void Go()
        {
            SetPhysicsProcessInternal(true);
            SetProcessInternal(true);
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
            _dog.PlayAnimation("jump");
        }
		
        private void RunTo(Vector2 direction)
        {
            if (_dog.MoveState == MoveState.Jump)
            {
                direction.x = 0;
            }
            direction = direction.Normalized();
            direction *= Speed * SpeedBoost;
            _velocity += direction;
        }
		
        private void Turn(bool left = true)
        {
            if (_dog.MoveState != MoveState.Jump)
            {
                float phi = TurnSpeed * GetPhysicsProcessDeltaTime();
                if (left) phi = -phi;
                _forwardDirection = _forwardDirection.Rotated(phi);
                _dog.Rotate(phi);
            }
        }

    }
}
