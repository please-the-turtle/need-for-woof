using Godot;

namespace NeedForWoof.Level
{
    /// <summary>
    /// The base class of the player's person.
    /// </summary>
    public abstract class Dog : KinematicBody2D
    {
        [Signal]
        public delegate void Finished();

        // TODO Dog id property
        ///<value>
        ///Multiplayer identificator.<br/> 
        ///Used for setting and searching info about dog.
        ///</value>
        public int Id { get; set; }

        [Export(PropertyHint.Range, "0, 500, or_greater")]
        public float MaxStamina
        {
            get => _maxStamina;
            set
            {
                if (value > 0)
                {
                    _maxStamina = value;
                }
            }
        }

        ///<value>
        ///Refill of stamina in units per second.
        ///</value>
        [Export(PropertyHint.Range, "0, 500, or_greater")]
        public float StaminaRefill
        {
            get => _staminaRefill;
            set
            {
                if (value > 0)
                {
                    _staminaRefill = value;
                }
            }
        }

        ///<value>
        ///Current amount of stamina.
        ///</value>
        public float Stamina
        {
            get => _stamina;
            set
            {
                if (value > _maxStamina)
                {
                    _stamina = _maxStamina;
                }
                else if (value < 0)
                {
                    _stamina = 0;
                }
                else
                {
                    _stamina = value;
                }
            }
        }

        public int Score = 0;

        public MoveState MoveState = MoveState.Run;

        private AnimationPlayer _animationPlayer;
        private DogMovement _movement;

        private float _stamina;
        private float _maxStamina = 500f;
        private float _staminaRefill = 25f;

        public override void _Ready()
        {
            Stamina = MaxStamina;

            _animationPlayer = GetNode<AnimationPlayer>("Visualization/AnimationPlayer");
            _animationPlayer.CurrentAnimation = "run";
            _movement = GetNode<DogMovement>("DogMovement");
            _movement.Go();
        }

        public override void _Process(float delta)
        {
            base._Process(delta);

            Stamina += StaminaRefill * delta;
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

        public void Finish()
        {
            if (GetTree().HasNetworkPeer())
            {
                Network network = GetNode<Network>("/root/Network");
                int networkId = GetTree().GetNetworkUniqueId();
                network.Rpc(nameof(network.UpdatePlayerStatus), networkId, PlayerStatus.NotReady);
            }

            MoveState = MoveState.Idle;
            _animationPlayer.CurrentAnimation = "idle";
            _movement.Stop();
            EmitSignal(nameof(Finished));
        }

    }

    public enum MoveState
    {
        Run = 0,
        Jump = 1,
        Idle = 2
    }
}
