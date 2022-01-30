using Godot;
using System.Collections.Generic;

namespace NeedForWoof.LobbyScreen
{
    public class PlayersList : VBoxContainer
    {
        private Dictionary<int, LobbyPlayerInfo> _playersInfo; 

        public override void _Ready()
        {
            _playersInfo = new Dictionary<int, LobbyPlayerInfo>();
            Global global = GetNode<Global>("/root/Global");

            foreach (var connectedPlayer in global.Network.ConnectedPlayers)
            {
                int id = connectedPlayer.Key;
                PlayerInfo info = connectedPlayer.Value;
                PackedScene lobbyInfo = ResourceLoader.Load<PackedScene>("res://Scenes/Menu/LobbyPlayerInfo.tscn");
                LobbyPlayerInfo newLobbyInfo = (LobbyPlayerInfo)lobbyInfo.Instance();

                AddChild(newLobbyInfo);
                newLobbyInfo.UpdateInfo(info);
                _playersInfo.Add(id, newLobbyInfo);
            }

            global.Network.Connect(nameof(Network.onConnectedPlayersUpdated), this, nameof(UpdateList));
        }

        public void UpdateList(int id, PlayerInfo playerInfo)
        {
            if(!_playersInfo.ContainsKey(id))
            {
                PackedScene lobbyInfo = ResourceLoader.Load<PackedScene>("res://Scenes/Menu/LobbyPlayerInfo.tscn");
                LobbyPlayerInfo newLobbyInfo = (LobbyPlayerInfo)lobbyInfo.Instance();

                AddChild(newLobbyInfo);
                _playersInfo.Add(id, newLobbyInfo);
            }

            _playersInfo[id].UpdateInfo(playerInfo);
        }
    }
}

