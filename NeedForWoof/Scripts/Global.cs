using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace NeedForWoof.Scripts
{
    public class Global : Node
    {
        private const string SaveGameFilePath = "user://save.game";
        
        public static string Nickname { get; set; } = "GoodBoy";

        public static int NetworkPort { get; set; } = 1502;

        public Node CurrentScene { get; set; }

        public override void _Ready()
        {
            // TODO loading settings
            // LoadGameSettings();

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

        

        // TODO Create autoadding of the changed settings
        public static void SaveGameSettings()
        {
            var gameSettings = new Godot.Collections.Dictionary<string, object>();
            gameSettings.Add(nameof(Nickname), Nickname);
            gameSettings.Add(nameof(NetworkPort), NetworkPort);
            
            SaveGameValues(gameSettings);
        }

        private static void SaveGameValues(params object[] values)
        {
            File saveFile = new File();
            saveFile.Open(SaveGameFilePath, File.ModeFlags.Write);
            
            saveFile.StoreLine(JSON.Print(values));
            
            saveFile.Close();
        }

        private void LoadGameSettings()
        {
            var gameValues = LoadGameValues();
            GD.Print(gameValues.ToString());
            Nickname = (string)gameValues[nameof(Nickname)];
            NetworkPort = (int) gameValues[nameof(NetworkPort)];
        }
        
        // TODO Fix saveFile parsing
        private static Godot.Collections.Dictionary<string, object> LoadGameValues()
        {
            File saveFile = new File();
            saveFile.Open(SaveGameFilePath, File.ModeFlags.Read);

            if (!saveFile.FileExists(SaveGameFilePath))
            {
                SaveGameSettings();
            }

            var jsonParseResult = JSON.Parse(saveFile.GetLine()).Result;
            Godot.Collections.Dictionary<string, object> gameValues = 
                new Godot.Collections.Dictionary<string, object>((Dictionary) jsonParseResult);
            
            saveFile.Close();

            return gameValues;
        }
    }
    
    
}
