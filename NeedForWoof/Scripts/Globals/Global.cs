using Godot;
using NeedForWoof.Scripts.Globals;

namespace NeedForWoof.Scripts
{
    public class Global : Node
    {
        public GameSettings GameSettings;

        public GameDataSaver Saver;
        public GameDataLoader Loader;
        
        private Node _currentScene;

        private GameFilePaths _filePaths;

        public override void _Ready()
        {
            _filePaths = new GameFilePaths();

            Loader = new GameDataLoader(_filePaths);
            Saver = new GameDataSaver(_filePaths);

            GameSettings = Loader.LoadGameSettings();

            Viewport root = GetTree().Root;
            _currentScene = root.GetChild(root.GetChildCount() - 1);
        }
        
        public void GotoScene(string path)
        {
            CallDeferred(nameof(DeferredGotoScene), path);
        }

        private void DeferredGotoScene(string path)
        {
            _currentScene.Free();
            PackedScene nextScene = (PackedScene)GD.Load(path);
            _currentScene = nextScene.Instance();
            GetTree().Root.AddChild(_currentScene);
            GetTree().CurrentScene = _currentScene;
        }
    }
    
    
}
