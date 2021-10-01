using Godot;
using NeedForWoof.Scripts.Globals;

namespace NeedForWoof.Scripts.MainMenu
{
    public class HostButton : Button
    {
        public void OnHostButton_pressed()
        {
            // Network server = GetNode<Network>("/root/Network");
            // var error = server.CreateServer();

            // if (error == Error.Ok)
            // {
                Global global = GetNode<Global>("/root/Global");
                global.GotoScene("res://Scenes/Menu/LobbyScreen.tscn");
            // }
            // else
            // {
            //     ErrorLabel errorLabel = GetParent().GetNode<ErrorLabel>("ErrorLabel");
            //     errorLabel.DisplayError(error.ToString());
            // }
            
        }
    }
}
