using Godot;

namespace NeedForWoof.ConnectScreen
{
    public class ConnectButton : Button
    {
        private Network _network;
        private string _ip;

        private ErrorLabel _errorLabel;

        public override void _Ready()
        {
            base._Ready();

            GetTree().Connect("connected_to_server", this, nameof(GotoLobbyScreen));
            GetTree().Connect("connection_failed", this, nameof(onConnectionFailed));

            _errorLabel = GetParent().GetNode<ErrorLabel>("ErrorLabel");
            _network = GetNode<Network>("/root/Network/");
        }

        private void OnConnectButton_pressed()
        {
            _ip = GetParent().GetNode<LineEdit>("DialogWindowFrame/IpAddressLine").Text;

            ConnectToHost(_ip);
        }

        private void onConnectionFailed()
        {
            _network.Close();
            _errorLabel.DisplayError("Host not found");
        }

        public void ConnectToHost(string _ip)
        {
            Error error = _network.CreateClient(_ip);

            if (error != Error.Ok)
            {
                _errorLabel.DisplayError(error.ToString());
                _network.Close();
            }
        }

        private void GotoLobbyScreen()
        {
            Global global = GetNode<Global>("/root/Global");

            global.GameSettings.LastIpAddress = _ip;
            GameDataSaver.SaveGameSettingsAsync(global.GameSettings);

            global.GotoScene("res://Scenes/Menu/LobbyScreen.tscn");
        }
    }
}
