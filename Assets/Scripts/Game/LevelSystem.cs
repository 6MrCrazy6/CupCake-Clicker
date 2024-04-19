using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SaveSystem;

namespace Game
{
    public class LevelSystem : MonoBehaviour
    {
        public TextMeshProUGUI levelText;
        public Slider levelSlider;

        public int level = 1;
        public int exp = 0;
        public int expToNextLevel = 50;

        private SaveManager saveManager;

        void Start()
        {
            saveManager = FindObjectOfType<SaveManager>();
            if (saveManager != null)
            {
                saveManager.LoadSettings();
                level = saveManager.gameData.level;
                exp = saveManager.gameData.exp;
                expToNextLevel = saveManager.gameData.expToNextLevel;
            }

            UpdateLevel();
        }

        void Update()
        {
            if(exp > expToNextLevel)
            {
                level++;
                exp = 0;
                expToNextLevel *= 2;
                UpdateLevel();

                if (saveManager != null)
                {
                    saveManager.gameData.level = level;
                    saveManager.gameData.exp = exp;
                    saveManager.gameData.expToNextLevel = expToNextLevel;
                    saveManager.SaveSettings();
                }
            }

        }

        public void UpdateLevel()
        {
            levelText.text = "Level " + level;
            levelSlider.maxValue = expToNextLevel; 
            levelSlider.value = exp; 
        }
    }
}

