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

        public MoveState MoveState
        {
            get => _moveState;
            set
            {
                switch (value)
                {
                    case MoveState.Idle:
                        _animationPlayer.CurrentAnimation = "idle";
                        _movement.Stop();
                        _moveState = MoveState.Idle;
                        break;
                    case MoveState.Run:
                        _animationPlayer.CurrentAnimation = "run";
                        _movement.Go();
                        _moveState = MoveState.Run;
                        break;
                    case MoveState.Jump:
                        _animationPlayer.CurrentAnimation = "jump";
                        _moveState = MoveState.Jump;
                        break;
                }
            }
        }

        private AnimationPlayer _animationPlayer;
        private DogMovement _movement;

        private float _stamina;
        private float _maxStamina = 500f;
        private float _staminaRefill = 25f;
        private MoveState _moveState;

        public override void _Ready()
        {
            Stamina = MaxStamina;

            _animationPlayer = GetNode<AnimationPlayer>("Visualization/AnimationPlayer");
            _movement = GetNode<DogMovement>("DogMovement");
            MoveState = MoveState.Idle;

            Countdown countdown = GetNode<Countdown>("../../../Countdown");
            countdown.Connect(nameof(Countdown.CountdownIsOver), this, nameof(Run));

            if (GetTree().HasNetworkPeer())
            {
                Network network = GetNode<Network>("/root/Network");
                int networkId = GetTree().GetNetworkUniqueId();
                network.Rpc(nameof(network.UpdatePlayerStatus), networkId, PlayerStatus.Ready);
            }

        }

        public override void _Process(float delta)
        {
            base._Process(delta);

            Stamina += StaminaRefill * delta;
        }

        // Used inside animation player.
        private void Run()
        {
            MoveState = MoveState.Run;
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
            EmitSignal(nameof(Finished));
        }
    }
}
