using Godot;

namespace NeedForWoof.LobbyScreen
{
    public class IpLabel : Label
    {
        public override void _Ready()
        {
            if (GetTree().IsNetworkServer())
            {
                Text = GetLocalIpString();
            }
            else
            {
                Visible = false;
            }
        }
        
        private string GetLocalIpString()
        {
            string ip = IP.GetLocalAddresses().ToString();

            RegEx regex = new RegEx();
            regex.Compile(@"192.168.\d+.\d+");
            
            var regexSearchResult = regex.Search(ip);
            if (regexSearchResult is null)
            {
                return "Network not found.";
            }

            return regexSearchResult.GetString();
        }
    }
}
