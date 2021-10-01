using Godot;
using NeedForWoof.Scripts.Globals;

namespace NeedForWoof.Scripts
{
    public class Global : Node
    {
        private GameFilePaths FilePaths;

        public GameSettings GameSettings;

        public GameDataSaver Saver;
        public GameDataLoader Loader;
        
        public Node CurrentScene { get; set; }

        public override void _Ready()
        {
            FilePaths = new GameFilePaths();

            Loader = new GameDataLoader(FilePaths);
            Saver = new GameDataSaver(FilePaths);

            GameSettings = Loader.LoadGameSettings();

            Viewport root = GetTree().Root;
            CurrentScene = root.GetChild(root.GetChildCount() - 1);
        }
        
        public void GotoScene(string path)
        {
            CallDeferred(nameof(DeferredGotoScene), path);
        }

        private void DeferredGotoScene(string path)
        {
            CurrentScene.Free();
            PackedScene nextScene = (PackedScene)GD.Load(path);
            CurrentScene = nextScene.Instance();
            GetTree().Root.AddChild(CurrentScene);
            GetTree().CurrentScene = CurrentScene;
        }
    }
    
    
}
