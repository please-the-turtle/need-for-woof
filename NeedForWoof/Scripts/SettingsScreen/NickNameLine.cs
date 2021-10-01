using Godot;

namespace NeedForWoof.Scripts.SettingsScreen
{
    public class NickNameLine : LineEdit
    {
        private Global global;

        public override void _Ready()
        {
            base._Ready();

            global = GetNode<Global>("/root/Global");

            Text = global.GameSettings.Nickname;
        }

        public void OnNickNameLine_text_changed(string newText)
        {
            global.GameSettings.Nickname = newText;
        }
    }
}
