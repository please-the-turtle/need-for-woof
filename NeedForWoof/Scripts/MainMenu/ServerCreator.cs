using Godot;

namespace NeedForWoof.Scripts.MainMenu
{
    public class ServerCreator : Control
    {
        public void CreateServer()
        {
            var peer = new NetworkedMultiplayerENet();
            var error = peer.CreateServer(1502, 4);
            GD.Print(error + " " + IP.GetLocalAddresses());

            if (error == Error.Ok)
            {
                GetTree().NetworkPeer = peer;
                            
                var global = GetNode<Global>("/root/Global");
                global.GotoScene("res://Scenes/Menu/LobbyScreen.tscn");
            }
            else
            {
                var errorLabel = GetNode<ErrorLabel>("ErrorLabel");
                errorLabel.DisplayError(error.ToString());
            }
            
        }
    }
}
