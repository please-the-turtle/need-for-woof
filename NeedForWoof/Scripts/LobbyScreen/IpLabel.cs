using Godot;

namespace NeedForWoof.Scripts.LobbyScreen
{
    public class IpLabel : Label
    {
        public override void _Ready()
        {
            Text = GetLocalIpString();
        }
        
        private string GetLocalIpString()
        {
            string ip = IP.GetLocalAddresses().ToString();

            RegEx regex = new RegEx();
            regex.Compile(@"192.168.\d+.\d+");
            
            var regexSearchResult = regex.Search(ip);
            if (regexSearchResult is null)
            {
                return "Local network not found.";
            }

            return regexSearchResult.GetString();
        }
    }
}
