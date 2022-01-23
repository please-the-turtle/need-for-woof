using Godot;

namespace NeedForWoof.LobbyScreen
{
    public class StatusIconTextureRect : TextureRect
    {
        private Texture _atlas = new ImageTexture();
        private AtlasTexture _readyTexture = new AtlasTexture();
        private AtlasTexture _notReadyTexture = new AtlasTexture();
        private AtlasTexture _hostTexture = new AtlasTexture();

        public override void _Ready()
        {
            SetIconsTextures();
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

        private void SetIconsTextures()
        {
            _atlas = (Texture)GD.Load("res://Textures/Menu/main_menu.png");

            _readyTexture.Atlas = _atlas;
            _readyTexture.Region = new Rect2(89, 89, 7, 7);

            _notReadyTexture.Atlas = _atlas;
            _notReadyTexture.Region = new Rect2(80, 89, 7, 7);

            _hostTexture.Atlas = _atlas;
            _hostTexture.Region = new Rect2(71, 89, 7, 7);
        }
    }

}
