using Godot;

namespace NeedForWoof.Scripts.LobbyScreen
{
    public class IpLabel : Label
    {
        public override void _Ready()
        {
            if (GetTree().IsNetworkServer())
            {
                string ip = GetLocalIpString();
                Text = ip;
            }
            else
            {
                Visible = false;
            }
        }

        private string GetLocalIpString()
        {
            RegEx regex = new RegEx();
            regex.Compile(@"192.168.\d+.\d+");
            string ip = IP.GetLocalAddresses().ToString();
            var regexSearchResult = regex.Search(ip);

            return regexSearchResult.GetString();
        }
    }
}
