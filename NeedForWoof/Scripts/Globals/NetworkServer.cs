using System;
using Godot;

namespace NeedForWoof
{
    public class NetworkServer : Network
    {
        public const int MaxPlayers = 4;

        public NetworkServer() : base()
        {
            NetworkedMultiplayerENet peer = new NetworkedMultiplayerENet();
            Global global = GetNode<Global>("/root/Global");
            int port = global.GameSettings.NetworkPort;
            peer.CreateServer(port, MaxPlayers);
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