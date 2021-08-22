namespace NeedForWoof.Scripts
{
    public struct PlayerInfo
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
