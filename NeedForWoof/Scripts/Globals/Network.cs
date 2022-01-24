using System.Collections.Generic;
using Godot;

namespace NeedForWoof
{
    public abstract class Network : Node
    {
        /// <value> Dictionary with info about connected players </value>
        public Dictionary<int, PlayerInfo> ConnectedPlayers { get; private set; }

        public Network()
        {
            GetTree().Connect("network_peer_connected", this, nameof(PlayerConnected));
            GetTree().Connect("network_peer_disconnected", this, nameof(PlayerDisconnected));
            GetTree().Connect("connected_to_server", this, nameof(ConnectedOk));
            GetTree().Connect("connection_failed", this, nameof(ConnectedFail));
            GetTree().Connect("server_disconnected", this, nameof(ServerDisconnected));
        }

        public virtual void Close()
        {
            GetTree().NetworkPeer = null;

            GetTree().Disconnect("network_peer_connected", this, nameof(PlayerConnected));
            GetTree().Disconnect("network_peer_disconnected", this, nameof(PlayerDisconnected));
            GetTree().Disconnect("connected_to_server", this, nameof(ConnectedOk));
            GetTree().Disconnect("connection_failed", this, nameof(ConnectedFail));
            GetTree().Disconnect("server_disconnected", this, nameof(ServerDisconnected));
        }

        /// <summary>For server and clients event.</summary>
        internal abstract void PlayerConnected(int id);
        
        /// <summary>For server and clients event.</summary>
        internal abstract void PlayerDisconnected(int id);

        /// <summary>For clients event.</summary>
        internal abstract void ConnectedOk();

        /// <summary>For clients event.</summary>
        internal abstract void ConnectedFail();

        /// <summary>For clients event.</summary>
        internal abstract void ServerDisconnected();

    }
}