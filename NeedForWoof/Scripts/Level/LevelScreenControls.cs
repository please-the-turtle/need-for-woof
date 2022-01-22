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

        float settingsScreenRatio = GetSettingsScreenRatio();
        if(OS.WindowSize.x / OS.WindowSize.y >= settingsScreenRatio)
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
        float offset = windowHeight / heightScale - canvasHeight;

        return offset;
    }

    private float GetWidthOffset()
    {
        int canvasWidth = (int)ProjectSettings.GetSetting("display/window/size/width");
        float widthScale = GetViewport().GetFinalTransform().Scale.x;
        float windowWidth = OS.WindowSize.x;
        float offset = windowWidth / widthScale - canvasWidth;

        return offset;
    }

    ///<summary>Returns screen ratio from the project settings.</summary>
    private float GetSettingsScreenRatio()
    {
        float width = (int)ProjectSettings.GetSetting("display/window/size/width");
        float height = (int)ProjectSettings.GetSetting("display/window/size/height");
        float ratio = width / height;

        return ratio;
    }
}
