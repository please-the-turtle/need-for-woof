using Godot;

namespace NeedForWoof.Scripts
{
    public class MoveButtons : Node2D
    {
        public override void _Ready()
        {
            PutButtonsInTheBottom();
        }

        private void PutButtonsInTheBottom()
        {
            int canvasHeight = (int)ProjectSettings.GetSetting("display/window/size/height");
            float heightScale = GetViewport().GetFinalTransform().Scale.y;
            float windowHeight = OS.WindowSize.y;
            float heightOffset = (windowHeight - canvasHeight * heightScale) / heightScale;
            Position += new Vector2(0, heightOffset);
        }
    }
}
