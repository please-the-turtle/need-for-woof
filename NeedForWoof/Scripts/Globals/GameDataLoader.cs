using System.Text.Json;
using Godot;

namespace NeedForWoof.Scripts.Globals
{
    public class GameDataLoader
    {
        private GameFilePaths _filePaths;

        public GameDataLoader(GameFilePaths filePaths)
        {
            _filePaths = filePaths;
        }

        public GameSettings LoadGameSettings()
        {
            var saveFile = new File();
            var error = saveFile.Open(_filePaths.GameSettingsFilePath, File.ModeFlags.Read);
            if (error == Error.Ok)
            {
                string json = saveFile.GetAsText();
                GameSettings settings = JsonSerializer.Deserialize<GameSettings>(json);
                saveFile.Close();
                return settings;
            }

            return new GameSettings();
        }
    }
}