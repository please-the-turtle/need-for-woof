namespace NeedForWoof
{
    public class GameSettings
    {
        public string Nickname
        {
            get => _nickname;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _nickname = value;
                }
            }
        }

        public int NetworkPort { get; set; } = 1502;

        public float SoundsVolume { get; set; } = 50f;

        public float MusicVolume { get; set; } = 50f;

        public string LastIpAddress 
        { 
            get => _lastIp;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _lastIp = value;
                }
            } 
        }

        private string _nickname = "GoodBoy";
        private string _lastIp = "192.168.1.68";
    }
}