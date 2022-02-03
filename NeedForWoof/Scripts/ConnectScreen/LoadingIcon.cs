using Godot;

namespace NeedForWoof.ConnectScreen
{

    public class LoadingIcon : TextureRect
    {
        private AnimationPlayer _animation;

        public override void _Ready()
        {
            _animation = GetNode<AnimationPlayer>("AnimationPlayer");
            
            GetTree().Connect("connection_failed", this, nameof(HideIcon));
            Network network = GetNode<Network>("/root/Network");
            network.Connect(nameof(Network.PeerChanged), this, nameof(ShowIcon));
        }

        private void ShowIcon()
        {
            Visible = true;
            _animation.Play("rotate");
        }

        private void HideIcon()
        {
            Visible = false;
            _animation.Stop();
            _animation.Seek(0);
        }
    }
}


