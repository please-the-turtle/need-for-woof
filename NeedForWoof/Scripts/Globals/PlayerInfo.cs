using Godot;

namespace NeedForWoof
{
    public class PlayerInfo : Object
    {
        public string Nickname { get; set; } = "not_set";

        public PlayerStatus Status { get; set; } = PlayerStatus.NotReady;

        public DogType Dog { get; set; } = DogType.Siba;

        public PlayerInfo Copy()
        {
            PlayerInfo newInfo = new PlayerInfo();

            newInfo.Nickname = Nickname;
            newInfo.Status = Status;
            newInfo.Dog = Dog;

            return newInfo;
        }
    }
}
