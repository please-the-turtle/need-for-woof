using Godot;

namespace NeedForWoof.Scripts.ConnectScreen
{
    public class ConnectButton : Button
    {
        public void OnConnectButton_pressed()
        {
            string ip = GetParent().GetNode<LineEdit>("DialogWindowFrame/IpAddressLine").Text;
            
            Network server = GetNode<Network>("/root/Network");
            var error = server.ConnectToServer(ip);

            if (error == Error.Ok)
            {
                Global global = GetNode<Global>("/root/Global");
                global.GotoScene("res://Scenes/Menu/LobbyScreen.tscn");
            }
            else
            {
                ErrorLabel errorLabel = GetParent().GetNode<ErrorLabel>("ErrorLabel");
                errorLabel.DisplayError(error.ToString());
            }

        }
    }
}
