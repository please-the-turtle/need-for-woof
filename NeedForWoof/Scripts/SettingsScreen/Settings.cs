using Godot;
using NeedForWoof.Scripts.Globals;

namespace NeedForWoof.Scripts.SettingsScreen
{
    public class Settings : Control
    {
        public void SaveSettings()
        {
            var global = GetNode<Global>("/root/Global");
            global.Saver.SaveGameSettings(global.GameSettings);
        }
    }
}
