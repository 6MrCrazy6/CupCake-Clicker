using TMPro;
using UnityEngine;
using SaveSystem;

namespace Game
{
    public class AutoClicker : MonoBehaviour
    {
        private ScoreAdd scoreAdd;
        private SaveManager saveManager;
        public TextMeshProUGUI autoClickersText;
        public TextMeshProUGUI priceAutoClickers;

        private int autoClickerPrice = 50;
        private float clickInterval = 1.0f;

        public int autoClickersCount = 0; 
        private bool isAutoClicking = false; 
        private float timer = 0.0f;

        private void Start()
        {
            scoreAdd = FindObjectOfType<ScoreAdd>();
            saveManager = FindObjectOfType<SaveManager>();

            if (saveManager != null)
            {
                saveManager.LoadSettings();
                autoClickerPrice = saveManager.gameData.priceAutoClicker;
                autoClickersCount = saveManager.gameData.autoClickersCount;
                if(autoClickersCount > 0)
                {
                    ToggleAutoClicker(true);
                }
                else
                {
                    ToggleAutoClicker(false);
                }
            }
        }

        void Update()
        {
            if (isAutoClicking)
            {
                timer += Time.deltaTime;
                if (timer >= clickInterval)
                {
                    for (int i = 0; i < autoClickersCount; i++)
                    {
                        scoreAdd.OnButtonDown();
                        scoreAdd.OnButtonUp();
                    }
                    timer = 0.0f; 
                }
            }

            autoClickersText.gameObject.SetActive(autoClickersCount >= 1);
            autoClickersText.text = "AutoClickers: " + autoClickersCount.ToString();
            priceAutoClickers.text = autoClickerPrice.ToString();
        }

        
        public void ToggleAutoClicker(bool enable)
        {
            isAutoClicking = enable;
        }


        public void BuyAutoClicker()
        {
            if (scoreAdd.currentScore >= autoClickerPrice)
            {
                autoClickersCount++; 
                scoreAdd.currentScore -= autoClickerPrice; 
                autoClickerPrice *= 2;

                ToggleAutoClicker(true);

                if (saveManager != null)
                {
                    saveManager.gameData.priceAutoClicker = autoClickerPrice;
                    saveManager.gameData.autoClickersCount = autoClickersCount;
                    saveManager.SaveSettings();
                }
            }
        }
    }
}

