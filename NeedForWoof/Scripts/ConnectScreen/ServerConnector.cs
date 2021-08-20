using Godot;

namespace NeedForWoof.Scripts.ConnectScreen
{
    public class ServerConnector : Control
    {
        public void ConnectToServer(string adress)
        {
            var peer = new NetworkedMultiplayerENet();
            var error = peer.CreateClient(adress, 1502);
            GD.Print(error);

            if (error == Error.Ok)
            {
                GetTree().NetworkPeer = peer;
                            
                var global = GetNode<Global>("/root/Global");
                global.GotoScene("res://Scenes/Menu/LobbyScreen.tscn");
            }
            else
            {
                var errorLabel = GetNode<Label>("ErrorLabel");
                errorLabel.Text = error.ToString();
                errorLabel.Visible = true;
            }
            
        }
        
        public void ConnectToServer()
        {
            LineEdit ipLine = GetNode<LineEdit>("DialogWindowFrame/IpAdressLine");
            string ip = ipLine.Text;
            ConnectToServer(ip);
        }
    }
}
