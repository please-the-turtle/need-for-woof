using Godot;

namespace NeedForWoof.MainMenu
{
    public class HostButton : Button
    {
        public void OnHostButton_pressed()
        {
            Global global = GetNode<Global>("/root/Global");
            // TODO Creating errors.
            GetNode<Network>("/root/Network").CreateServer();
            global.GotoScene("res://Scenes/Menu/LobbyScreen.tscn");
        }
    }
}
