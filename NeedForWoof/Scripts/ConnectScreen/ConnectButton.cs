using Godot;

namespace NeedForWoof.ConnectScreen
{
    public class ConnectButton : Button
    {
        public void OnConnectButton_pressed()
        {
            string ip = GetParent().GetNode<LineEdit>("DialogWindowFrame/IpAddressLine").Text;
            
            Global global = GetNode<Global>("/root/Global");
            global.Network = new NetworkClient(ip);
            global.GotoScene("res://Scenes/Menu/LobbyScreen.tscn");
        }
    }
}
