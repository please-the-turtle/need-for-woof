using Godot;

namespace NeedForWoof.LobbyScreen
{
    public class StartButton : Button
    {
        private TextureRect _iconPlay;
        private TextureRect _iconWait;

        public override void _Ready()
        {
            _iconPlay = GetNode<TextureRect>("IconPlay");
            _iconWait = GetNode<TextureRect>("IconWait");
        }

        [Remote]
        private void OnStartButton_toggled(bool buttonPressed)
        {
            _iconPlay.Visible = !buttonPressed;
            _iconWait.Visible = buttonPressed; 

            Network network = GetNode<Network>("/root/Network");
            if (GetTree().IsNetworkServer())
            {
                if (network.AllPlayersAreReady())
                {
                    network.Rpc(nameof(network.StartLevel));
                }
                else
                {
                    Pressed = false;
                }
            }
            else
            {
                int networkId = GetTree().GetNetworkUniqueId();
                PlayerStatus status = (buttonPressed) ? PlayerStatus.Ready : PlayerStatus.NotReady;
                network.Rpc(nameof(network.UpdatePlayerStatus), networkId, status);
            }
        }
    }
}
