using Godot;

namespace NeedForWoof.Scripts.CommonControls
{
    public class ErrorLabel : Label
    {
        public void DisplayError(string errorText)
        {
            Text = errorText;
            Visible = true;
        }
    }
}
