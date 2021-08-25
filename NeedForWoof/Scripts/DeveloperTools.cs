using System.Linq;
using Godot;

namespace NeedForWoof.Scripts
{
    public class DeveloperTools : Control
    {
        private LineEdit SceneNameLine;

        public override void _Ready()
        {
            base._Ready();
            
            SceneNameLine = GetNode<LineEdit>("DialogWindowFrame/VBoxContainer/ChangeSceneContainer/SceneNameLine");
        }

        public void ExecuteCommand()
        {
            if (!string.IsNullOrEmpty(SceneNameLine.Text))
            {
                string sceneName = SceneNameLine.Text;
                if (!sceneName.EndsWith(".tscn"))
                {
                    sceneName = "res://Scenes/" + sceneName + ".tscn";
                }
                File scene = new File();
                if (scene.FileExists(sceneName))
                {
                    var global = GetNode<Global>("/root/Global");
                    global.GotoScene(sceneName);
                }
            }
        }
    }
}
