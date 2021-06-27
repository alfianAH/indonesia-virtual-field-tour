using Audio;
using Effects;
using UnityEngine;
using UnityEngine.UI;

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

        [Header("Mute Handler")] 
        [SerializeField] private Image audioSettings;
        [SerializeField] private Sprite muteSprite;
        [SerializeField] private Sprite unmuteSprite;
        [SerializeField] private bool isMute;
        
        #region Pause and Resume Buttons

        /// <summary>
        /// Pause the game 
        /// </summary>
        public void PauseGame()
        {
            StartCoroutine(FadingEffect.FadeIn(pauseCanvasGroup, 
                afterEffect: () =>
                {
                    Time.timeScale = 0f;
                    // Pause the audio
                    AudioManager.Instance.Pause();
                })
            );
        }
        
        /// <summary>
        /// Resume the game
        /// </summary>
        public void ResumeGame()
        {
            StartCoroutine(FadingEffect.FadeOut(pauseCanvasGroup, 
                () => Time.timeScale = 1f,
                () => AudioManager.Instance.UnPause())
            );
        }

        #endregion

        #region Mute and Unmute Buttons
        
        /// <summary>
        /// Mute audio
        /// </summary>
        public void MuteAudio()
        {
            isMute = !isMute;
            
            // Change sprite
            audioSettings.sprite = isMute ? muteSprite : unmuteSprite;
            // Mute audio
            AudioManager.Instance.Mute(isMute);
        }

        #endregion

        #region Info Button
        
        /// <summary>
        /// Show information
        /// </summary>
        public void ShowInfo()
        {
            StartCoroutine(FadingEffect.FadeIn(infoCanvasGroup));
        }
        
        /// <summary>
        /// Hide information
        /// </summary>
        public void HideInfo()
        {
            StartCoroutine(FadingEffect.FadeOut(infoCanvasGroup));
        }

        #endregion
    }
}