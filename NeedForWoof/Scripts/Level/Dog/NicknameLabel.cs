using Godot;

namespace NeedForWoof.Level
{
    public class NicknameLabel : Label
    {
        public override void _Ready()
        {
            // TODO rewrite
            Network network = GetNode<Network>("/root/Network");
            Dog dog = GetNode<Dog>("../..");
            Text = network.GetPlayerInfoById(dog.GetNetworkMaster()).Nickname;
        }
    }
}
