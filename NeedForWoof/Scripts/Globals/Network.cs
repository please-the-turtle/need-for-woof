using System.Collections.Generic;
using Godot;

namespace NeedForWoof
{
    public abstract class Network : Node
    {
        [Signal]
        public delegate void onConnectedPlayersUpdated(int changedPlayerId, PlayerInfo newPlayerInfo);

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
            SetNetworkPeer();

            _connectedPlayers = new Dictionary<int, PlayerInfo>();
            
            GetTree().Connect("network_peer_connected", this, nameof(PlayerConnected));
            GetTree().Connect("network_peer_disconnected", this, nameof(PlayerDisconnected));
            GetTree().Connect("connected_to_server", this, nameof(ConnectedOk));
            GetTree().Connect("connection_failed", this, nameof(ConnectedFail));
            GetTree().Connect("server_disconnected", this, nameof(ServerDisconnected));
        }

        public virtual void Close()
        {
            if(GetTree().IsNetworkServer())
            {
                NetworkedMultiplayerENet eNet = GetTree().NetworkPeer as NetworkedMultiplayerENet;
                eNet.CloseConnection();
            }
            GetTree().NetworkPeer = null;
        }

        /// <summary>Sets new network peer. Calls then Network node is "ready".</summary>
        protected abstract void SetNetworkPeer();

        /// <summary>For server and clients event.</summary>
        protected virtual void PlayerConnected(int playerId)
        {
            int id = GetTree().GetNetworkUniqueId();
            PlayerInfo info = new PlayerInfo();
            Global global = GetNode<Global>("/root/Global");
            string nickname = global.GameSettings.Nickname;
            info.Nickname = nickname;

            RpcId(playerId, nameof(global.Network.UpdateConnectedPlayers), id, info);
        }
        
        /// <summary>For server and clients event.</summary>
        protected abstract void PlayerDisconnected(int id);

        /// <summary>For clients event.</summary>
        protected virtual void ConnectedOk()
        {
        }

        /// <summary>For clients event.</summary>
        protected abstract void ConnectedFail();

        /// <summary>For clients event.</summary>
        protected abstract void ServerDisconnected();

        [Remote]
        public void UpdateConnectedPlayers(int playerId, PlayerInfo newInfo)
        {
            // Network network = GetNode<Network>("/root/Global/_network");
            // network._connectedPlayers.Add(playerId, newInfo);
            GD.Print("pizda");
            // EmitSignal(nameof(onConnectedPlayersUpdated), playerId, newInfo);
        }

    }
}
