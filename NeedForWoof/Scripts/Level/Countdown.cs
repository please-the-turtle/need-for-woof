using Godot;
using NeedForWoof;
using System.Collections.Generic;

public class Countdown : CanvasLayer
{
	[Signal]
	public delegate void CountdownIsOver();

	public float TickTimeInterval => .857f;

	public float StartDelay => .857f * 4;

	private Label _label;
	private AnimationPlayer _animation;

	private List<string> _labelStrings = new List<string> { "3", "2", "1", "GO!" };
	private Queue<string> _labelsQueue = new Queue<string>();

	private Timer _timer = new Timer();

	private Network _network;

	public override void _Ready()
	{
		_label = GetNode<Label>("Label");
		_animation = GetNode<AnimationPlayer>("AnimationPlayer");

		_timer = new Timer();
		_timer.WaitTime = TickTimeInterval;
		_timer.OneShot = true;
		AddChild(_timer);
		_timer.Connect("timeout", this, nameof(Tick));

		_network = GetNode<Network>("/root/Network");
		_network.Connect(nameof(Network.PlayerChangedStatus), this, nameof(onPlayerChangedStatus));

		_label.Visible = false;
	}

	private void onPlayerChangedStatus(int playerId, PlayerStatus status)
	{
		if (_network.AllPlayersAreReady())
		{
			Start();
		}
	}

	public void Start()
	{
		foreach (var text in _labelStrings)
		{
			_labelsQueue.Enqueue(text);
		}

		_timer.Start(StartDelay);
	}

	private void Tick()
	{
		_label.Text = _labelsQueue.Dequeue();
		_label.Visible = true;
		
		_animation.Play("tick");

		if (_labelsQueue.Count > 0)
		{
			_timer.Start(TickTimeInterval);
		}
		else
		{
			EmitSignal(nameof(CountdownIsOver));
		}
	}
}
