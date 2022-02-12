using Godot;
using System.Collections.Generic;

public class Countdown : Control
{
    [Signal]
    public delegate void CountdownIsOver();

    [Export(PropertyHint.Range, "0, 2, or_greater")]
    public float TickTimeInterval { get; set; } = .857f;

    [Export(PropertyHint.Range, "0, 5, or_greater")]
    public float StartDelay { get; set; } = .857f * 4;

    private Label _label;
    private AnimationPlayer _animation;

    private List<string> _labelStrings = new List<string> { "3", "2", "1", "GO!" };
    private Queue<string> _labelsQueue = new Queue<string>();

    private Timer _timer = new Timer();

    public override void _Ready()
    {
        _label = GetNode<Label>("Label");
        _animation = GetNode<AnimationPlayer>("AnimationPlayer");

        _timer = new Timer();
        _timer.WaitTime = TickTimeInterval;
        _timer.OneShot = true;
        AddChild(_timer);
        _timer.Connect("timeout", this, nameof(Tick));

        Visible = false;
        Start();
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
        Visible = true;
        
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
