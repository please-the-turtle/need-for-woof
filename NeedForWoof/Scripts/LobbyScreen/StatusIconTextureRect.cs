using Godot;

namespace NeedForWoof.LobbyScreen
{
    public class StatusIconTextureRect : TextureRect
    {
        [Export]
        private Texture _readyTexture;

        [Export]
        private Texture _notReadyTexture;

        [Export]
        private Texture _hostTexture;

        public override void _Ready()
        {
            base._Ready();

            SetStatusIcon(PlayerStatus.NotReady);
        }

        public void SetStatusIcon(PlayerStatus status)
        {
            switch(status)
            {
                case PlayerStatus.Ready:
                    Texture = _readyTexture;
                    break;
                case PlayerStatus.NotReady:
                    Texture = _notReadyTexture;
                    break;
                case PlayerStatus.Host:
                    Texture = _hostTexture;
                    break;
            }
        }
    }

}
