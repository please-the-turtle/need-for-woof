using Godot;

namespace NeedForWoof
{
    public class ErrorLabel : Label
    {
        private Timer _timer;

        public override void _Ready()
        {
            base._Ready();

            _timer = new Timer();
            _timer.WaitTime = 2;
            _timer.OneShot = true;
            _timer.Connect("timeout", this, nameof(HideError));
            AddChild(_timer);
        }

        public void DisplayError(string errorText)
        {
            _timer.Start();
            Text = errorText;
            Visible = true;
        }

        private void HideError()
        {
            Visible = false;
        }
    }
}
