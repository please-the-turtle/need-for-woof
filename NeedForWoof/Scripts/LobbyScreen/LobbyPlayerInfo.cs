using Godot;
using System;

namespace NeedForWoof.LobbyScreen
{
    public class LobbyPlayerInfo : Control
    {
        private Label _nicknameLabel;
        private TextureRect _dogIcon;
        private StatusIconTextureRect _statusIcon;

        public override void _Ready()
        {
            _nicknameLabel = GetNode<Label>("Nickname");
            _dogIcon = GetNode<TextureRect>("DogIcon");
            _statusIcon = GetNode<StatusIconTextureRect>("StatusIcon");
        }

        public void UpdateInfo(PlayerInfo playerInfo)
        {
            _nicknameLabel.Text = playerInfo.Nickname;
            // TODO _dogIcon = ...
            _statusIcon.SetStatusIcon(playerInfo.Status);
        }
    }
}