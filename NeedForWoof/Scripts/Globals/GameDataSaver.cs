using System.Text.Json;
using Godot;

namespace NeedForWoof.Scripts.Globals
{
    public class GameDataSaver
    {
        private GameFilePaths _filePaths;

        public GameDataSaver(GameFilePaths filePaths)
        {
            _filePaths = filePaths;
        }

        public void SaveGameSettings(GameSettings settings)
        {
            SaveData(_filePaths.GameSettingsFilePath, settings);
        }

        private void SaveData(string saveFilePath, object data)
        {
            File dataFile = new File();     
            
            dataFile.Open(saveFilePath, File.ModeFlags.Write);
            string json = JsonSerializer.Serialize(data);
            dataFile.StoreLine(json);
            dataFile.Close();
        }
    }
}