using UnityEngine;
using System.IO;
using Game;

namespace SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        public GameData gameData;

        public void LoadSettings()
        {
            gameData = JsonUtility.FromJson<GameData>(File.ReadAllText(Application.streamingAssetsPath + "/GameData.json"));
        }

        public void SaveSettings()
        {
            File.WriteAllText(Application.streamingAssetsPath + "/GameData.json", JsonUtility.ToJson(gameData));
        }

    }
}


