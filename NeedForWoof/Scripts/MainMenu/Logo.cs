using Godot;

namespace NeedForWoof.Scripts.MainMenu
{
    public class Logo : TextureButton
    {
        private Timer _timer;
        private int _touchCount = 0;
        
        public override void _Ready()
        {
            _timer = new Timer();
            _timer.Autostart = false;
            _timer.OneShot = true;
            
            AddChild(_timer);

            _timer.Connect("timeout", this, nameof(ResetTouchCount));
            Connect("pressed", this, nameof(OnLogo_click));
        }

        private void OnLogo_click()
        {
            _touchCount++;
            _timer.WaitTime = .5f;
            _timer.Start();
            if (_touchCount > 4)
            {
                var global = GetNode<Global>("/root/Global");
                global.GotoScene("Scenes/Menu/DevToolsScreen.tscn");
            }
        }

        private void ResetTouchCount()
        {
            _touchCount = 0;
        }
    }
}
