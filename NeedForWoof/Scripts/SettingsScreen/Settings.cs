using Godot;

namespace NeedForWoof.SettingsScreen
{
    public class Settings : Control
    {
        public void SaveSettings()
        {
            var global = GetNode<Global>("/root/Global");
            GameDataSaver.SaveGameSettingsAsync(global.GameSettings);
        }
    }
}
