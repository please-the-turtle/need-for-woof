using Godot;

namespace NeedForWoof.Scripts.LobbyScreen
{
    public class IpLabel : Label
    {
        public override void _Ready()
        {
            var ip = IP.GetLocalAddresses();
            Text = ip.ToString();
        }
    }
}
