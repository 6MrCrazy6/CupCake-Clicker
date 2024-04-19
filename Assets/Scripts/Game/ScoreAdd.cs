using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SaveSystem;

namespace Game
{
    public class ScoreAdd : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        private LevelSystem levelSystem;

        public float currentScore = 0;
        public int tabPower = 1;
        private float scoreIncreasePerSecond = 1;
        private float x = 0f;

        private Vector2 originalSize;
        private RectTransform rectTransform;
        private AudioSource audioSource;
        private Button button;

        private float scaleFactor = 0.9f;
        private float animationDuration = 0.1f;

        private SaveManager saveManager;

        void Start()
        {
            originalSize = GetComponent<RectTransform>().localScale;
            rectTransform = GetComponent<RectTransform>();
            levelSystem = FindObjectOfType<LevelSystem>();
            saveManager = FindObjectOfType<SaveManager>();
            button = GetComponent<Button>();
            audioSource = GetComponent<AudioSource>();
            
            button.onClick.AddListener(OnButtonDown);
            button.onClick.AddListener(OnButtonUp);

            LoadSavedData();
        }


        void Update()
        {
            scoreText.text = currentScore.ToString();
            scoreIncreasePerSecond = x * Time.deltaTime;
            currentScore = currentScore + scoreIncreasePerSecond;
        }

        void LoadSavedData()
        {
            if (saveManager != null)
            {
                saveManager.LoadSettings();
                currentScore = saveManager.gameData.currentScore;
                tabPower = saveManager.gameData.tabPower;
            }
        }

        public void OnButtonDown()
        {
            StartCoroutine(AnimateButton());
            audioSource.Play();
        }

        public void OnButtonUp()
        {
            currentScore += tabPower;
            levelSystem.exp++;
            levelSystem.UpdateLevel();

            SaveData();
        }

        void SaveData()
        {
            if (saveManager != null)
            {
                saveManager.gameData.currentScore = currentScore;
                saveManager.gameData.exp = levelSystem.exp;
                saveManager.SaveSettings();
            }
        }

        IEnumerator AnimateButton()
        {
            Vector2 targetSize = originalSize * scaleFactor;

            float timer = 0f;
            while (timer < animationDuration)
            {
                rectTransform.localScale = Vector2.Lerp(originalSize, targetSize, timer / animationDuration);
                timer += Time.deltaTime;
                yield return null;
            }

            rectTransform.localScale = targetSize;

            yield return new WaitForSeconds(0.1f);

            timer = 0f;
            while (timer < animationDuration)
            {
                rectTransform.localScale = Vector2.Lerp(targetSize, originalSize, timer / animationDuration);
                timer += Time.deltaTime;
                yield return null;
            }

            rectTransform.localScale = originalSize;
        }

    }
}

