using Godot;

namespace NeedForWoof.Scripts.SettingsScreen
{
    public class SoundSlider : HSlider
    {
        private Global _global;
    
        public override void _Ready()
        {
            base._Ready();

            _global = GetNode<Global>("/root/Global");
            Value = _global.GameSettings.SoundsVolume;
        }
        
        public void OnSoundSlider_value_changed(float value)
        {
            _global.GameSettings.SoundsVolume = value;
        }
    }
}
