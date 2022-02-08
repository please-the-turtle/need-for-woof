using Godot;

namespace NeedForWoof.Level
{
    public class NicknameLabel : Label
    {
        public override void _Ready()
        {
            Global global = GetNode<Global>("/root/Global");
            Text = global.GameSettings.Nickname;
        }
    }
}
