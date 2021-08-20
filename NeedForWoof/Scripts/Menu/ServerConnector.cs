using Godot;

namespace NeedForWoof.Scripts.Menu
{
    public class ServerConnector : Control
    {
        public void ConnectToServer(string adress)
        {
            var global = GetNode<Global>("/root/Global");
            global.GotoScene("res://Scenes/Menu/LobbyScreen.tscn");
        }
        
        public void ConnectToServer()
        {
            LineEdit ipLine = GetNode<LineEdit>("DialogWindowFrame/IpAdressLine");
            string ip = ipLine.Text;
            ConnectToServer(ip);
        }
    }
}
