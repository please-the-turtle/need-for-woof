using Newtonsoft.Json;
using Godot;

namespace NeedForWoof
{
    public static class GameDataLoader
    {
        public static GameSettings LoadGameSettings()
        {
            var saveFile = new File();
            var error = saveFile.Open(GameFilePaths.GameSettingsFilePath, File.ModeFlags.Read);
            if (error == Error.Ok)
            {
                string json = saveFile.GetAsText();
                GameSettings settings = JsonConvert.DeserializeObject<GameSettings>(json);
                saveFile.Close();
                return settings;
            }

            return new GameSettings();
        }
    }
}