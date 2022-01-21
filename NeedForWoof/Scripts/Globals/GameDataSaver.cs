using Newtonsoft.Json;
using System.Threading.Tasks;
using Godot;

namespace NeedForWoof
{
    public static class GameDataSaver
    {
        public static void SaveGameSettings(GameSettings settings)
        {
            SaveData(GameFilePaths.GameSettingsFilePath, settings);
        }

        public async static void SaveGameSettingsAsync(GameSettings settings)
        {
            await Task.Run(() => SaveGameSettings(settings));
        }

        private static void SaveData(string saveFilePath, object data)
        {
            File dataFile = new File();     
            
            dataFile.Open(saveFilePath, File.ModeFlags.Write);
            string json = JsonConvert.SerializeObject(data);
            dataFile.StoreLine(json);
            dataFile.Close();
        }
    }
}