using Godot;

namespace NeedForWoof.ConnectScreen
{
    public class ConnectButton : Button
    {
        private Network _network;

        private Label _errorLabel;

        public override void _Ready()
        {
            base._Ready();

            GetTree().Connect("connected_to_server", this, nameof(GotoLobbyScreen));
            GetTree().Connect("connection_failed", this, nameof(onConnectionFailed));

            _errorLabel = GetParent().GetNode<Label>("ErrorLabel");
            _network = GetNode<Network>("/root/Network/");
        }

        private void OnConnectButton_pressed()
        {
            string ip = GetParent().GetNode<LineEdit>("DialogWindowFrame/IpAddressLine").Text;

            ConnectToHost(ip);
        }

        private void onConnectionFailed()
        {
            _network.Close();
            _errorLabel.Text = "Host not found";
            _errorLabel.Visible = true;
        }

        private void GotoLobbyScreen()
        {
            Global global = GetNode<Global>("/root/Global");
            global.GotoScene("res://Scenes/Menu/LobbyScreen.tscn");
        }

        public void ConnectToHost(string ip)
        {
            Error error = _network.CreateClient(ip);

            if (error != Error.Ok)
            {
                // TODO error timer
                // Timer timer = new Timer();
                // timer.Start(2);
                // timer.OneShot = true;
                // timer.Connect(nameof());
                _errorLabel.Text = error.ToString();
                _errorLabel.Visible = true;
                _network.Close();
            }
        }
    }
}
