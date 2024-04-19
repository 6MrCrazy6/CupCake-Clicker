using TMPro;
using UnityEngine;
using SaveSystem;

namespace Game
{
    public class UpdateTab : MonoBehaviour
    {
        public TextMeshProUGUI updateText;
        private int updatePrice = 30;

        private ScoreAdd scoreAdd;
        private SaveManager saveManager;

        void Start ()
        {
            scoreAdd = FindObjectOfType<ScoreAdd>();
            saveManager = FindObjectOfType<SaveManager>();

            if (saveManager != null)
            {
                saveManager.LoadSettings();
                updatePrice = saveManager.gameData.priceTabUpdate;
            }
        }

        void Update()
        {
            updateText.text = updatePrice.ToString();
        }

        public void UpdateClick()
        {
            if(scoreAdd.currentScore >= updatePrice)
            {
                scoreAdd.currentScore -= updatePrice;
                scoreAdd.tabPower *= 2;
                updatePrice *= 3;

                if (saveManager != null)
                {
                    saveManager.gameData.priceTabUpdate = updatePrice;
                    saveManager.gameData.tabPower = scoreAdd.tabPower;
                    saveManager.SaveSettings();
                }
            }
        }
    }
}

