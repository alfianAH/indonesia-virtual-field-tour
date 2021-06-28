using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneLoading
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Text loadingValueText;
        
        private void Start()
        {
            loadingValueText.text = "Memuat 0%";
            StartCoroutine(LoadSceneAsync());
        }

        /// <summary>
        /// Load scene asynchronously
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadSceneAsync()
        {
            // Load scene asynchronously
            AsyncOperation loadScene = 
                SceneManager.LoadSceneAsync(LoadingData.SceneName);
            
            while (!loadScene.isDone)
            {
                float progress = Mathf.Clamp01(loadScene.progress / 0.9f);
                loadingValueText.text = $"Memuat {progress * 100}%";
                yield return null;
            }
        }
    }
}