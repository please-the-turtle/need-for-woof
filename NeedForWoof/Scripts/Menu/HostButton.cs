using Godot;

namespace NeedForWoof.Scripts.Menu
{
    public class HostButton : TextureButton
    {
        public void OnHostButton_pressed()
        {
            Global global = GetNode<Global>("/root/Global");
            global.GotoScene("res://Scenes/Menu/LobbyScreen.tscn");
        }
    }
}
