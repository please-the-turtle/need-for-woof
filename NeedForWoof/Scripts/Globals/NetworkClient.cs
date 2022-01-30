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

        protected override void SetNetworkPeer()
        {
            NetworkedMultiplayerENet peer = new NetworkedMultiplayerENet();
            Global global = GetNode<Global>("/root/Global");
            int port = global.GameSettings.NetworkPort;
            peer.CreateClient(_ip, port);
            GetTree().NetworkPeer = peer;
        }

        protected override void PlayerConnected(int id)
        {
            base.PlayerConnected(id);
            GD.Print($"Connected: {id}.");
        }

        protected override void PlayerDisconnected(int id)
        {
            GD.Print($"Disonnected: {id}.");
        }

        // protected override void ConnectedOk()
        // {
        //     base.ConnectedOk();
        //     GD.Print($"Connected OK.");
        // }

        protected override void ConnectedFail()
        {
            GD.Print($"Connected fail.");
        }

        protected override void ServerDisconnected()
        {
            GD.Print($"Server disconnected.");
        }
    }
}