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

        public void SetNickname(string nickname)
        {
            _nicknameLabel.Text = nickname;
        }

        public void SetStatusIcon(PlayerStatus playerStatus)
        {
            _statusIcon.SetStatusIcon(playerStatus);
        }
    }
}