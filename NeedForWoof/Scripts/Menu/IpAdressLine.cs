using Godot;

namespace NeedForWoof.Scripts.Menu
{
    public class IpAdressLine : LineEdit
    {
        public void OnIpAdressLine_focus_entered()
        {
            int lastSymbolIndex = Text.Length - 1;
            int lastDotIndex = lastSymbolIndex;
            while (Text[lastDotIndex] != '.')
            {
                lastDotIndex--;
            }

            CaretPosition = lastSymbolIndex;
            Select(lastSymbolIndex, lastDotIndex + 1);
        }
    }
}
