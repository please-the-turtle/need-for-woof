using Godot;

namespace NeedForWoof
{
    public class PlayerInfo : Object
    {
        public string Nickname { get; set; } = "not_set";
        public PlayerStatus Status { get; set; } = PlayerStatus.NotReady;
    }
}
