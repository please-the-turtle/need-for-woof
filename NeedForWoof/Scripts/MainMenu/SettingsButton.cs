using Godot;

namespace NeedForWoof.MainMenu
{
    public class SettingsButton : Button
    {
        public void OnSettingsButton_pressed()
        {
            var global = GetNode<Global>("/root/Global");
            global.GotoScene("res://Scenes/Menu/SettingsScreen.tscn");
        }
    }
}
