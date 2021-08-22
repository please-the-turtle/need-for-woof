using Godot;

namespace NeedForWoof.Scripts.LobbyScreen
{
    public class NickNameLabel : Label
    {
        public override void _Ready()
        {
            Text = Global.Nickname;
        }
    }
}
