using Godot;

namespace NeedForWoof.Scripts
{
    public class PlayerInfo : Object
    {
        public string Nickname;
        public PlayerStatus Status;
    }

    public enum PlayerStatus
    {
        Ready,
        NotReady,
        Host
    }
}
