using Godot;

namespace NeedForWoof.MainMenu
{
    public class HostButton : Button
    {
        public void OnHostButton_pressed()
        {
            Global global = GetNode<Global>("/root/Global");
            global.Network = new NetworkServer();
            global.GotoScene("res://Scenes/Menu/LobbyScreen.tscn");
        }
    }
}
