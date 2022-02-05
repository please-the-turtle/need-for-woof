using Godot;

namespace NeedForWoof.LobbyScreen
{
    public class StatusLabel : Label
    {
        private const string _readyString = "Players are ready";
        private const string _notReadyString = "Not all players are ready";

        private Network _network;

        public override void _Ready()
        {
            _network = GetNode<Network>("/root/Network");
            _network.Connect(nameof(Network.PlayerChangedStatus), this, nameof(ChangeStatusLine));

            ChangeStatusLine();
        }

        private void ChangeStatusLine(int playerId = 0, PlayerStatus status = PlayerStatus.NotReady)
        {
            if (_network.AllPlayersAreReady())
            {
                Text = _readyString;
            }
            else
            {
                Text = _notReadyString;
            }
        }
    }
}

