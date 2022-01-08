using Godot;

public class LevelScreenControls : CanvasLayer
{
    private TouchScreenButton _runRightButton;
    private TouchScreenButton _runLeftButton;
    private TouchScreenButton _jumpButton;
    private TouchScreenButton _menuButton;

    public override void _Ready()
    {
        _runRightButton = GetNode<TouchScreenButton>("RunRightButton");
        _runLeftButton = GetNode<TouchScreenButton>("RunLeftButton");
        _jumpButton = GetNode<TouchScreenButton>("JumpButton");
        _menuButton = GetNode<TouchScreenButton>("MenuButton");
        // GetTree().Root.Connect("size_changed", this, "LocateButtons");
        LocateButtons();
    }

    private void LocateButtons()
    {
        PutButtonInTheBottom(_runRightButton);
        PutButtonInTheBottom(_runLeftButton);
        PutButtonInTheBottom(_jumpButton);
        if(OS.WindowSize.x > OS.WindowSize.y)
        {
            PutButtonToTheRight(_runRightButton);
            PutButtonToTheRight(_menuButton);
        }
    }

    private void PutButtonInTheBottom(TouchScreenButton button)
    {
        float heightOffset = GetHeightOffset();
        button.Position += new Vector2(0, heightOffset);
    }

    private void PutButtonToTheRight(TouchScreenButton button)
    {
        float widthOffset = GetWidthOffset();
        button.Position += new Vector2(widthOffset, 0);
    }

    private float GetHeightOffset()
    {
        int canvasHeight = (int)ProjectSettings.GetSetting("display/window/size/height");
        float heightScale = GetViewport().GetFinalTransform().Scale.y;
        float windowHeight = OS.WindowSize.y;
        return windowHeight / heightScale - canvasHeight;
    }

    private float GetWidthOffset()
    {
        int canvasWidth = (int)ProjectSettings.GetSetting("display/window/size/width");
        float widthScale = GetViewport().GetFinalTransform().Scale.x;
        float windowWidth = OS.WindowSize.x;
        return windowWidth / widthScale - canvasWidth;
    }
}
