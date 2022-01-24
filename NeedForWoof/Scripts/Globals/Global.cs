using System;
using Godot;

namespace NeedForWoof
{
    public class Global : Node
    {
        public GameSettings GameSettings;

        public Node CurrentScene { get; private set; }

        public Network Network 
        {
            get => _network; 
            set
            {
                if(value == null)
                {
                    throw new NullReferenceException("Network must not be null. For closing network use Global.CloseNetworkConnection().");
                }
                if(_network != null)
                {
                    _network.Close();
                }
                _network = value;
            } 
        }

        private Network _network = null;

        public override void _Ready()
        {
            OS.RequestPermissions();
            GameSettings = GameDataLoader.LoadGameSettings();

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

        public void CloseNetworkConnection()
        {
            Network.Close();
            _network = null;
        }
    }
}
