using Godot;
using Godot.Collections;

namespace NeedForWoof.Scripts
{
    public class Network : Node
    {
        public int MaxClients { get; set; } = 3;

        public Dictionary<int, PlayerInfo> PlayersDictionary;

        public override void _Ready()
        {
            base._Ready();

            PlayersDictionary = new Dictionary<int, PlayerInfo>();
            PlayersDictionary.Add(1, new PlayerInfo(){ Nickname = Global.Nickname, Status = PlayerStatus.Host });

            // GetTree().Connect("network_peer_connected", this, nameof(PlayerConnected));
            GetTree().Connect("network_peer_disconnected", this, nameof(PlayerDisconnected));
            GetTree().Connect("server_disconnected", this, nameof(LeaveServer));
            GetTree().Connect("connected_to_server", this, nameof(PlayerConnected));
        }

        public Error CreateServer()
        {
            var peer = new NetworkedMultiplayerENet();
            var error = peer.CreateServer(Global.NetworkPort, MaxClients);

            if (error == Error.Ok)
            {
                GetTree().NetworkPeer = peer;
            }

            return error;
        }
        
        public void RemoveServer()
        {
            var peer = (NetworkedMultiplayerENet) GetTree().NetworkPeer;
            peer.CloseConnection();
            GetTree().NetworkPeer = null;
        }

        private void PlayerConnected(int playerId)
        {
            GD.Print("connected: " + playerId);
        }

        // [Remote]
        // private PlayerInfo RegisterPlayer()
        // {
        //     int id = GetTree().NetworkPeer.GetUniqueId();
        //     Rpc();
        // }
        
        private void PlayerDisconnected(int playerId)
        {
            PlayersDictionary.Remove(playerId);
            GD.Print("disconnected: " + playerId);
        }
        
        public Error ConnectToServer(string address)
        {
            var peer = new NetworkedMultiplayerENet();
            var error = peer.CreateClient(address, Global.NetworkPort);

            if (error == Error.Ok)
            {
                GetTree().NetworkPeer = peer;
            }
            
            return error;
        }
        
        public void LeaveServer()
        {
            var peer = (NetworkedMultiplayerENet) GetTree().NetworkPeer;
            peer.CloseConnection();
            GetTree().NetworkPeer = null;
        }
    }
}
