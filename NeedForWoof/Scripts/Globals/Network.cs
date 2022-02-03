using System.Collections.Generic;
using Godot;

namespace NeedForWoof
{
    public class Network : Node
    {
        [Signal] public delegate void NewPlayerLogged(int loggedPlayerId, string nickname);
        [Signal] public delegate void PlayerChangedStatus(int changedPlayerId, PlayerStatus status);
        [Signal] public delegate void PlayerLeft(int leftPlayerId);
        [Signal] public delegate void ConnectionClosed();
        [Signal] public delegate void PeerChanged();

        private Network(){}

        public int MaxPlayers { get; private set; } = 3;

        private Global _global;

        /// <value> Dictionary with info about connected players. </value>
        public Dictionary<int, PlayerInfo> ConnectedPlayers
        {
            get
            {
                return new Dictionary<int, PlayerInfo>(_connectedPlayers);
            }
        }

        protected Dictionary<int, PlayerInfo> _connectedPlayers;

        public override void _Ready()
        {
            _global = GetNode<Global>("/root/Global");
            _connectedPlayers = new Dictionary<int, PlayerInfo>();
            
            GetTree().Connect("network_peer_connected", this, nameof(PlayerConnected));
            GetTree().Connect("network_peer_disconnected", this, nameof(PlayerDisconnected));
            GetTree().Connect("connected_to_server", this, nameof(ConnectedOk));
            GetTree().Connect("connection_failed", this, nameof(ConnectedFail));
            GetTree().Connect("server_disconnected", this, nameof(ServerDisconnected));
        }

        public Error CreateServer()
        {
            if (GetTree().NetworkPeer != null)
            {
                Close();
            }

            // Create new network peer
            NetworkedMultiplayerENet peer = new NetworkedMultiplayerENet();
            int port = _global.GameSettings.NetworkPort;
            Error error = peer.CreateServer(port, MaxPlayers);
            if (error == Error.Ok)
            {
                GetTree().NetworkPeer = peer;
                EmitSignal(nameof(PeerChanged));

                int selfId = GetTree().GetNetworkUniqueId();
                string nickname = _global.GameSettings.Nickname;
                AddPlayer(selfId, nickname);
                UpdatePlayerStatus(selfId, PlayerStatus.Host);
            }

            return error;
        }

        public Error CreateClient(string ip)
        {
            if (GetTree().NetworkPeer != null)
            {
                Close();
            }

            // Create new network peer
            NetworkedMultiplayerENet peer = new NetworkedMultiplayerENet();
            int port = _global.GameSettings.NetworkPort;
            Error error = peer.CreateClient(ip, port);
            if (error == Error.Ok)
            {
                GetTree().NetworkPeer = peer;
                EmitSignal(nameof(PeerChanged));
                
                int selfId = GetTree().GetNetworkUniqueId();
                string nickname = _global.GameSettings.Nickname;
                AddPlayer(selfId, nickname);
            }

            return error;
        }

        public virtual void Close()
        {
            if (GetTree().NetworkPeer != null)
            {
                int selfId = GetTree().GetNetworkUniqueId();
                Rpc(nameof(DeletePlayer), selfId);

                if(GetTree().IsNetworkServer())
                {
                    NetworkedMultiplayerENet eNet = GetTree().NetworkPeer as NetworkedMultiplayerENet;
                    eNet.CloseConnection();
                }
                GetTree().NetworkPeer = null;

                _connectedPlayers.Clear();
                EmitSignal(nameof(ConnectionClosed));
            }
        }

        // For server and clients event.
        protected virtual void PlayerConnected(int playerId)
        {
            int selfId = GetTree().GetNetworkUniqueId();
            PlayerInfo selfInfo =  _connectedPlayers[selfId];

            RpcId(playerId, nameof(AddPlayer), selfId, selfInfo.Nickname);
            RpcId(playerId, nameof(UpdatePlayerStatus), selfId, selfInfo.Status);

            GD.Print($"Player connected: {playerId}.");
        }
        
        // For server and clients event.
        protected void PlayerDisconnected(int playerId)
        {
            if (_connectedPlayers.ContainsKey(playerId))
            {
                DeletePlayer(playerId);
            }

            GD.Print($"Player disconnected: {playerId}.");
        }

        protected void ConnectedOk()
        {
            GD.Print("Client connected OK.");
        }

        protected void ConnectedFail()
        {
            GD.Print("Client connected Fail.");
        }

        protected void ServerDisconnected()
        {
            Close();
            _global.GotoScene("res://Scenes/Menu/MainMenu.tscn");
        }

        [Remote]
        private void AddPlayer(int playerId, string nickname)
        {
            PlayerInfo info = new PlayerInfo();
            info.Nickname = nickname;
            _connectedPlayers.Add(playerId, info);

            Network network = GetNode<Network>("/root/Network");
            EmitSignal(nameof(NewPlayerLogged), playerId, nickname);
        }

        [Remote]
        public void UpdatePlayerStatus(int playerId, PlayerStatus status)
        {
            _connectedPlayers[playerId].Status = status;
            EmitSignal(nameof(PlayerChangedStatus), playerId, status);
        }

        [Remote]
        public void DeletePlayer(int playerId)
        {
            _connectedPlayers.Remove(playerId);
            EmitSignal(nameof(PlayerLeft), playerId);
        }

        public override void _Notification(int what)
        {
            if (what == MainLoop.NotificationCrash ||
                what == MainLoop.NotificationWmQuitRequest)
            {
                Close();
                GetTree().Quit();
            }
        }
    }
}
