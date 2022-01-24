using Godot;

namespace NeedForWoof
{
    public class NetworkClient : Network
    {
        private string _ip;

        public NetworkClient(string ip)
        {
            _ip = ip;
        }

        public override void _Ready()
        {
            base._Ready();

            NetworkedMultiplayerENet peer = new NetworkedMultiplayerENet();
            Global global = GetNode<Global>("/root/Global");
            int port = global.GameSettings.NetworkPort;
            peer.CreateClient(_ip, port);
            GetTree().NetworkPeer = peer;
        }

        internal override void PlayerConnected(int id)
        {
            GD.Print($"Connected: {id}.");
        }

        internal override void PlayerDisconnected(int id)
        {
            GD.Print($"Disonnected: {id}.");
        }

        internal override void ConnectedOk()
        {
            GD.Print($"Connected OK.");
        }

        internal override void ConnectedFail()
        {
            GD.Print($"Connected fail.");
        }

        internal override void ServerDisconnected()
        {
            GD.Print($"Server disconnected.");
        }
    }
}