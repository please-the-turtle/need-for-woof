using Godot;

namespace NeedForWoof.MainMenu
{
    public class ConnectButton : Button
    {
        public void OnConnectButton_pressed()
        {
            var global = GetNode<Global>("/root/Global");
            global.GotoScene("res://Scenes/Menu/ConnectScreen.tscn");
        }
    }
}
