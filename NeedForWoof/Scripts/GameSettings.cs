using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeedForWoof.Scripts
{
    public class GameSettings
    {   
        public string Nickname { get; set; } = "GoodBoy";

        public int NetworkPort { get; set; } = 1502;

        public float SoundsVolume { get; set; } = 50f;
        
        public float MusicVolume { get; set; } = 50f;
    }
}