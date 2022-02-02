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
            Network network = GetNode<Network>("/root/Network");

            network.Connect(nameof(Network.NewPlayerLogged), this, nameof(AddPlayerInfo));
            network.Connect(nameof(Network.PlayerChangedStatus), this, nameof(ChangePlayerStatusInfo));
            network.Connect(nameof(Network.PlayerLeft), this, nameof(DeletePlayerInfo));
            network.Connect(nameof(Network.ConnectionClosed), this, nameof(ClearList));

            foreach(var player in network.ConnectedPlayers)
            {
                int id = player.Key;
                string nickname = player.Value.Nickname;
                PlayerStatus status = player.Value.Status;
                AddPlayerInfo(id, nickname);
                ChangePlayerStatusInfo(id, status);
            }
        }

        private void AddPlayerInfo(int playerId, string nickname)
        {
            PackedScene lobbyInfo = ResourceLoader.Load<PackedScene>("res://Scenes/Menu/LobbyPlayerInfo.tscn");
            LobbyPlayerInfo newLobbyInfo = (LobbyPlayerInfo)lobbyInfo.Instance();
            AddChild(newLobbyInfo);
            _playersInfo.Add(playerId, newLobbyInfo);

            _playersInfo[playerId].SetNickname(nickname);
        }

        private void ChangePlayerStatusInfo(int playerId, PlayerStatus status)
        {
            _playersInfo[playerId].SetStatusIcon(status);
        }

        private void DeletePlayerInfo(int playerId)
        {
            RemoveChild(_playersInfo[playerId]);
            _playersInfo.Remove(playerId);
        }

        private void ClearList()
        {
            foreach(Node child in GetChildren())
            {
                RemoveChild(child);
            }
        }
    }
}

