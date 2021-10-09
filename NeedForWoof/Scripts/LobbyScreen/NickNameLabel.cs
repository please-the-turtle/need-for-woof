using Godot;

namespace NeedForWoof.Scripts.LobbyScreen
{
    public class NickNameLabel : Label
    {
        public override void _Ready()
        {
            var global = GetNode<Global>("/root/Global");
            Text = global.GameSettings.Nickname;
        }
    }
}
