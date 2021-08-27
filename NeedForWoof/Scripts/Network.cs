using Godot;
using Godot.Collections;

namespace NeedForWoof.Scripts
{
    public class Network : Node
    {
        public int MaxClients { get; set; } = 3;

        [Remote]
        public Dictionary<int, PlayerInfo> PlayersDictionary;

        [Remote]
        public PlayerInfo PlayerInfo
        {
            get
            {
                string nickname = Global.Nickname;
                PlayerStatus status;
                if (GetTree().NetworkPeer.GetUniqueId() == 1) status = PlayerStatus.Host;
                else status = PlayerStatus.NotReady;

                return new PlayerInfo() {Nickname = nickname, Status = status};
            }
        }

        public override void _Ready()
        {
            base._Ready();

            PlayersDictionary = new Dictionary<int, PlayerInfo>();
            PlayerInfo hostInfo = new PlayerInfo() {Nickname = Global.Nickname, Status = PlayerStatus.Host};
            PlayersDictionary.Add(1, hostInfo);

            GetTree().Connect("network_peer_connected", this, nameof(PlayerConnected));
            GetTree().Connect("network_peer_disconnected", this, nameof(PlayerDisconnected));
            GetTree().Connect("server_disconnected", this, nameof(LeaveServer));
            // GetTree().Connect("connected_to_server", this, nameof(PlayerConnected));
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
            GetTree().NetworkPeer = null;
            GD.Print("Server lost");
        }
        
        [Remote]
        private void PlayerConnected(int playerId)
        {
            var playerInfo = RpcId(playerId, nameof(PlayerInfo));
            Rpc(nameof(PlayersDictionary.Add), playerInfo);
            GD.Print(PlayersDictionary);
        }

        [Remote]
        private void PlayerDisconnected(int playerId)
        {
            PlayersDictionary.Remove(playerId);
            GD.Print(PlayersDictionary);
        }
    }
}
