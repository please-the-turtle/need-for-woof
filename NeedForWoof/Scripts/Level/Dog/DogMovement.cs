using Godot;

namespace NeedForWoof.Level
{
    /// <summary>
    /// Controls the dog movement.<br/>
    /// Must be the Dog node child.
    /// </summary>
    public class DogMovement : Node
    {
        /// <summary>
        /// Constant speed of the Dog.
        /// </summary>
        [Export(PropertyHint.Range, "1, 1000, or_greater")]
        public float Speed = 400;
		
        /// <summary>
        /// Angular rate of rotation. Measured in radians.
        /// <para><u>Turning methods are not used yet.</u></para>
        /// </summary>
        [Export(PropertyHint.Range, "0, 3")] 
        public float TurnSpeed = 1f;

        [Export(PropertyHint.Range, "0, 500, or_greater")]
        public float JumpStaminaExpenditure
        {
            get => _jumpStaminaExpenditure;
            set
            {
                if (value > 0)
                {
                    _jumpStaminaExpenditure = value;
                }
            }
        }

        /// <summary>
        /// BoostSpeed. Used for jumping. Controlled by the AnimationPlayer.
        /// </summary>
        public float SpeedBoost = 1f;
        
        private Dog _dog;
        
        private Vector2 _velocity = Vector2.Zero;
        private Vector2 _forwardDirection = Vector2.Up;
        private float _jumpStaminaExpenditure = 100;
        
        public override void _Ready()
        {
            _dog = GetParent<Dog>();
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            
            RunAhead();
            _dog.MoveAndSlide(_velocity);
            _velocity = Vector2.Zero;
        }
        
        /// <summary>
        /// Directs the dog to the right.
        /// </summary>
        [Sync]
        public void RunRight()
        {
            RunTo(Vector2.Right);
        }

        /// <summary>
        /// Directs the dog to the left.
        /// </summary>
        [Sync]
        public void RunLeft()
        {
            RunTo(Vector2.Left);
        }

        /// <summary>
        /// Makes the dog jump.
        /// </summary>
        [Sync]
        public void Jump()
        {
            if (_dog.Stamina >= JumpStaminaExpenditure && _dog.MoveState == MoveState.Run)
            {
                _dog.Stamina -= JumpStaminaExpenditure;
                _dog.PlayAnimation("jump");
            }
        }

        /// <summary>
        /// Makes the dog stop.
        /// </summary>
        public void Stop()
        {
            SetPhysicsProcess(false);
            SetProcess(false);
            _velocity = Vector2.Zero;
        }

        /// <summary>
        /// Makes the dog move.
        /// </summary>
        public void Go()
        {
            SetPhysicsProcessInternal(true);
            SetProcessInternal(true);
        }

        /// <summary>
        /// Directs the dog.
        /// </summary>
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
		
        /// <summary>
        /// Directs the dog forward.
        /// </summary>
        private void RunAhead()
        {
            RunTo(_forwardDirection);
        }

        // Turning methods are not used yet.
        private void TurnRight()
        {
            Turn(false);
        }

        private void TurnLeft()
        {
            Turn();
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
