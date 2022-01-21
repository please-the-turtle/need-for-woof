using Godot;

namespace NeedForWoof.SettingsScreen
{
    public class MusicSlider : HSlider
    {
        private Global _global;
        
        public override void _Ready()
        {
            base._Ready();

            _global = GetNode<Global>("/root/Global");
            Value = _global.GameSettings.MusicVolume;
        }
        
        public void OnMusicSlider_value_changed(float value)
        {
            _global.GameSettings.MusicVolume = value;
        }
    }
}
