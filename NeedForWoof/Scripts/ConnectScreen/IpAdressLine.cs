using Godot;

namespace NeedForWoof.ConnectScreen
{
    public class IpAdressLine : LineEdit
    {
        public override void _Ready()
        {
            base._Ready();

            Text = GetNode<Global>("/root/Global").GameSettings.LastIpAddress;
        }

        public void OnIpAdressLine_focus_entered()
        {
            // int lastSymbolIndex = Text.Length - 1;
            // int lastDotIndex = lastSymbolIndex;
            // while (Text[lastDotIndex] != '.')
            // {
            //     lastDotIndex--;
            // }

            // CaretPosition = lastSymbolIndex;
            // Select(lastSymbolIndex, lastDotIndex + 1);
        }
    }
}
