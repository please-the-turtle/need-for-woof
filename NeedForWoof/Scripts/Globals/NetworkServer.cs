using System;
using Godot;

namespace NeedForWoof
{
    public class NetworkServer : Network
    {
        public const int MaxPlayers = 4;

        protected override void SetNetworkPeer()
        {
            NetworkedMultiplayerENet peer = new NetworkedMultiplayerENet();
            Global global = GetNode<Global>("/root/Global");
            int port = global.GameSettings.NetworkPort;
            peer.CreateServer(port, MaxPlayers);
            GetTree().NetworkPeer = peer;
        }

        protected override void PlayerDisconnected(int id)
        {
            GD.Print($"Disonnected: {id}.");
        }

        protected override void ConnectedOk()
        {
            GD.Print($"Connected OK.");
        }

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
