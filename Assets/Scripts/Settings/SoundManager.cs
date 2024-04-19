using UnityEngine;
using UnityEngine.UI;
using SaveSystem;

namespace Setting
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;
        public SoundSaveManager soundSaveManager;

        public AudioSource backgroundMusic;
        public AudioSource soundEffect;

        public AudioClip backgroundClip;
        public AudioClip clickClip;

        public Slider musicSlider;
        public Slider soundSlider;

        private SoundSettings currentSettings;

        private void Awake()
        {
            soundSaveManager = FindObjectOfType<SoundSaveManager>();
            LoadSoundSettings();
        }

        private void OnApplicationQuit()
        {
            SaveSoundSettings();
        }

        private void LoadSoundSettings()
        {
            soundSaveManager.LoadSettings();
            currentSettings = soundSaveManager.soundSettings;

            if (currentSettings != null)
            {
                musicSlider.value = currentSettings.musicVolume;
                soundSlider.value = currentSettings.soundVolume;
                ApplySoundSettings(currentSettings);
            }
        }

        private void ApplySoundSettings(SoundSettings settings)
        {
            backgroundMusic.volume = settings.musicVolume;
            soundEffect.volume = settings.soundVolume;
        }

        private void SaveSoundSettings()
        {
            currentSettings.musicVolume = musicSlider.value;
            currentSettings.soundVolume = soundSlider.value;
            soundSaveManager.SaveSoundSettings();
        }

        public void PlayClickSound()
        {
            soundEffect.clip = clickClip;
            soundEffect.Play();
        }

        public void SetMusicVolume()
        {
            backgroundMusic.volume = musicSlider.value;
            currentSettings.musicVolume = musicSlider.value;
            soundSaveManager.SaveSoundSettings();
        }

        public void SetSoundVolume()
        {
            soundEffect.volume = soundSlider.value;
            currentSettings.soundVolume = soundSlider.value;
            soundSaveManager.SaveSoundSettings();
        }

    }
}

