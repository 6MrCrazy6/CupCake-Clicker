using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class StartButton : MonoBehaviour
    {
        public GameObject LoadScene;
        public Slider loadProgresBar;

        public void StartGame()
        {
            LoadScene.SetActive(true);

            StartCoroutine(LoadAsyncScene());
        }
        IEnumerator LoadAsyncScene()
        {
            AsyncOperation loadScene = SceneManager.LoadSceneAsync("Game");
            loadScene.allowSceneActivation = false;

            while (!loadScene.isDone)
            {
                loadProgresBar.value = loadScene.progress;

                if (loadScene.progress >= .9f && !loadScene.allowSceneActivation)
                {
                    yield return new WaitForSeconds(2f);
                    loadScene.allowSceneActivation = true;
                }
                yield return null;
            }

        }
    }
}

