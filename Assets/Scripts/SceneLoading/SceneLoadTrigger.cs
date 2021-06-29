using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneLoading
{
    public class SceneLoadTrigger : MonoBehaviour
    {
        #region Singleton

        private static SceneLoadTrigger instance;
        private const string LOG = nameof(SceneLoadTrigger);
        
        /// <summary>
        /// Singleton method
        /// </summary>
        public static SceneLoadTrigger Instance
        {
            get
            {
                if (instance == null)
                {
                    // Find instance
                    instance = FindObjectOfType<SceneLoadTrigger>();
                    
                    // If instance is not found, ...
                    if (instance == null)
                    {
                        // Give log error
                        Debug.LogError($"{LOG} not found");
                    }
                }

                return instance;
            }
        } 

        #endregion
        
        #region Don't Destroy On Load
        
        /// <summary>
        /// Use only 1 Scene Load Trigger from HomeScene
        /// </summary>
        private void SetInstance()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        #endregion

        public Animator transitionAnimator;

        [SerializeField] private Text progressText;
        [SerializeField] private float waitTime = 1.0f;
        private static readonly int StartKeyAnim = Animator.StringToHash("Start");
        private static readonly int EndKeyAnim = Animator.StringToHash("End");

        private void Awake()
        {
            SetInstance();
        }

        /// <summary>
        /// Load loading scene to trigger scene loader by name
        /// </summary>
        /// <param name="sceneName">Scene's name to load</param>
        public void LoadScene(string sceneName)
        {
            LoadingData.SceneName = sceneName;
            StartCoroutine(LoadLevel());
        }
        
        /// <summary>
        /// Load level asynchronously and play animation
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadLevel()
        {
            // Load scene async
            AsyncOperation loadScene = SceneManager.LoadSceneAsync(LoadingData.SceneName);
            // Don't allow scene activation
            loadScene.allowSceneActivation = false;
            
            // Play animation
            transitionAnimator.SetTrigger(StartKeyAnim);
            
            // Wait for transition
            yield return new WaitForSeconds(waitTime);
            
            // Update progress text
            if (!progressText.gameObject.activeInHierarchy)
                progressText.gameObject.SetActive(true);
            
            float progress = Mathf.Clamp01(loadScene.progress / 0.9f);
            progressText.text = $"Memuat {progress*100}%";
            
            // Wait until the loading is done
            yield return new WaitUntil(() => loadScene.progress >= 0.9f);

            // Load scene
            loadScene.allowSceneActivation = true;
            transitionAnimator.SetTrigger(EndKeyAnim);
            
            yield return null;
        }
    }
}