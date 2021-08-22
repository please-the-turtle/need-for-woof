using Godot;

namespace NeedForWoof.Scripts.SettingsScreen
{
    public class NickNameLine : LineEdit
    {
        public override void _Ready()
        {
            base._Ready();

            Text = Global.Nickname;
        }

        public void OnNickNameLine_text_changed(string newText)
        {
            Global.Nickname = newText;
        }
    }
}
