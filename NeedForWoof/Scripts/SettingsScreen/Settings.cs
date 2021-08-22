using Godot;

namespace NeedForWoof.Scripts.SettingsScreen
{
    public class Settings : Control
    {
        public void SaveSettings()
        {
            Global.SaveGameSettings();
        }
    }
}
