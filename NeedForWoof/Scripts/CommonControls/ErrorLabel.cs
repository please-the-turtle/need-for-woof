using Godot;

namespace NeedForWoof
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
