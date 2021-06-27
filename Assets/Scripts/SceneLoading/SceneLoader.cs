using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneLoading
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Text loadingValueText;
        
        private const float WAIT_SECONDS = 3.0f;
        
        private void Start()
        {
            loadingValueText.text = "Memuat 0%";
            StartCoroutine(LoadSceneAsync());
        }

        private void Update()
        {
            Debug.Log("A");
        }

        /// <summary>
        /// Load scene asynchronously
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadSceneAsync()
        {
            Debug.Log("waiting");
            // Wait for 3 seconds
            yield return new WaitForSeconds(WAIT_SECONDS);
            Debug.Log("Loading");
            // Load scene asynchronously
            AsyncOperation loadScene = 
                SceneManager.LoadSceneAsync(LoadingData.SceneName);
            
            while (!loadScene.isDone)
            {
                loadingValueText.text = $"Memuat {loadScene.progress * 100}%";
                yield return null;
            }
        }
    }
}