using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class ExitAndBackToMenuButtons : MonoBehaviour
    {
        public void ExitGame()
        {
            Application.Quit();
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

