using Godot;

namespace NeedForWoof.Scripts
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
