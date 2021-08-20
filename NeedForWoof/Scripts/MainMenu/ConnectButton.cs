using Godot;

namespace NeedForWoof.Scripts.MainMenu
{
    public class ConnectButton : TextureButton
    {
        public void OnConnectButton_pressed()
        {
            var global = GetNode<Global>("/root/Global");
            global.GotoScene("res://Scenes/Menu/ConnectScreen.tscn");
        }
    }
}
