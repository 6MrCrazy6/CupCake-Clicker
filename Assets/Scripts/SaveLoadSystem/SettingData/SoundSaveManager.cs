using System.IO;
using UnityEngine;
using Setting;

namespace SaveSystem
{
    public class SoundSaveManager : MonoBehaviour
    {
        public SoundSettings soundSettings;

        public void LoadSettings()
        {
            soundSettings = JsonUtility.FromJson<SoundSettings>(File.ReadAllText(Application.streamingAssetsPath + "/SoundData.json"));
        }

        public void SaveSoundSettings()
        {
            File.WriteAllText(Application.streamingAssetsPath + "/SoundData.json", JsonUtility.ToJson(soundSettings));
        }
    }
}

