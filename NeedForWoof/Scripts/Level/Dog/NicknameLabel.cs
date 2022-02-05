using Godot;

namespace NeedForWoof.Level
{
    public class NicknameLabel : Label
    {
        public override void _Ready()
        {
            Network network = GetNode<Network>("/root/Network");
            // Getting id from Dog node.
            int id = GetNode<Dog>("../..").Id;
            PlayerInfo selfInfo = network.GetPlayerInfoById(id);
            Text = selfInfo.Nickname;
        }
    }
}
