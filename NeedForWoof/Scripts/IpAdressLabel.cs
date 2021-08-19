using Godot;

namespace NeedForWoof.Scenes.Menu
{
    public class IpAdressLabel : Label
    {

        public override void _Ready()
        {
            Text = IP.GetLocalAddresses()[7].ToString();
        }

    }
}
