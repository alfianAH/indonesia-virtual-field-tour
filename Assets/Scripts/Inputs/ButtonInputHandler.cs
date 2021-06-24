using Effects;
using UnityEngine;

namespace Inputs
{
    public class ButtonInputHandler : MonoBehaviour
    {
        #region Singleton

        private static ButtonInputHandler instance;
        private const string LOG = nameof(ButtonInputHandler);
        
        /// <summary>
        /// Singleton method
        /// </summary>
        public static ButtonInputHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    // Find instance
                    instance = FindObjectOfType<ButtonInputHandler>();
                    
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
        
        [Header("Pause Handler")]
        [SerializeField] private CanvasGroup pauseCanvasGroup;

        [Header("Marker Info Handler")] 
        [SerializeField] private CanvasGroup infoCanvasGroup;
        
        #region Pause and Resume Buttons

        /// <summary>
        /// Pause the game 
        /// </summary>
        public void PauseGame()
        {
            StartCoroutine(FadingEffect.Fade(pauseCanvasGroup, 
                afterEffect:() => Time.timeScale = 0f)
            );
        }
        
        /// <summary>
        /// Resume the game
        /// </summary>
        public void ResumeGame()
        {
            StartCoroutine(FadingEffect.Fade(pauseCanvasGroup, 
                () => Time.timeScale = 1f)
            );
        }

        #endregion

        #region Info Button
        
        /// <summary>
        /// Show information
        /// </summary>
        public void ShowInfo()
        {
            StartCoroutine(FadingEffect.Fade(infoCanvasGroup));
        }
        
        /// <summary>
        /// Hide information
        /// </summary>
        public void HideInfo()
        {
            StartCoroutine(FadingEffect.Fade(infoCanvasGroup));
        }

        #endregion
    }
}