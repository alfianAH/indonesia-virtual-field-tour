using UnityEngine;
using UnityEngine.SceneManagement;

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
        
        private const string LOADING_SCENE_NAME = "LoadingScene";

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
            SceneManager.LoadScene(LOADING_SCENE_NAME);
        }
    }
}