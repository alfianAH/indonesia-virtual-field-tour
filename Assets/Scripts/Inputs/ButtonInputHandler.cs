using Audio;
using Effects;
using SceneLoading;
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
        [SerializeField] private RectTransform infoPanelTransform;

        [Header("Mute Handler")] 
        [SerializeField] private Image audioSettings;
        [SerializeField] private Sprite muteSprite;
        [SerializeField] private Sprite unmuteSprite;
        [SerializeField] private bool isMute;

        [Header("Exit Handler")] 
        [SerializeField] private CanvasGroup exitPanel;

        private EffectAudioManager effectAudioManager;

        private void Awake()
        {
            effectAudioManager = EffectAudioManager.Instance;
        }
        
        /// <summary>
        /// Play button click audio
        /// </summary>
        private void ButtonClickAudio()
        {
            effectAudioManager.Play(ListEffectSound.ButtonClick);
        }

        #region Pause and Resume Buttons

        /// <summary>
        /// Pause the game 
        /// </summary>
        public void PauseGame()
        {
            ButtonClickAudio();
            StartCoroutine(FadingEffect.FadeIn(pauseCanvasGroup, 
                afterEffect: () =>
                {
                    Time.timeScale = 0f;
                    // Pause the audio
                    ScriptAudioManager.Instance.Pause();
                })
            );
        }
        
        /// <summary>
        /// Resume the game
        /// </summary>
        public void ResumeGame()
        {
            ButtonClickAudio();
            StartCoroutine(FadingEffect.FadeOut(pauseCanvasGroup, 
                () => Time.timeScale = 1f,
                () => ScriptAudioManager.Instance.UnPause())
            );
        }

        #endregion

        #region Load Scene Button
        
        /// <summary>
        /// Play game
        /// </summary>
        public void PlayGame()
        {
            ButtonClickAudio();
            SceneLoadTrigger.Instance.LoadScene("MainScene");
        }
        
        /// <summary>
        /// Back to home button
        /// </summary>
        public void BackToHome()
        {
            ButtonClickAudio();
            Time.timeScale = 1f;
            ScriptAudioManager.Instance.StopAll();
            SceneLoadTrigger.Instance.LoadScene("HomeScene");
        }

        #endregion
        
        #region Load Link Button
        
        /// <summary>
        /// Open url link in other app
        /// </summary>
        /// <param name="url">URL Link</param>
        public void OpenLink(string url)
        {
            if(!string.IsNullOrWhiteSpace(url))
            {
                Application.OpenURL(url);
            }
        }
        
        #endregion

        #region Exit Game
        
        /// <summary>
        /// Show exit confirmation
        /// </summary>
        public void ShowExitConfirmation()
        {
            ButtonClickAudio();
            StartCoroutine(FadingEffect.FadeIn(exitPanel));
        }
        
        /// <summary>
        /// Hide exit confirmation
        /// </summary>
        public void HideExitConfirmation()
        {
            ButtonClickAudio();
            StartCoroutine(FadingEffect.FadeOut(exitPanel));
        }
        
        /// <summary>
        /// Exit game
        /// </summary>
        public void ExitGame()
        {
            ButtonClickAudio();
            Application.Quit();
        }

        #endregion

        #region Mute and Unmute Buttons
        
        /// <summary>
        /// Mute audio
        /// </summary>
        public void MuteAudio()
        {
            ButtonClickAudio();
            isMute = !isMute;
            
            // Change sprite
            audioSettings.sprite = isMute ? muteSprite : unmuteSprite;
            // Mute audio
            ScriptAudioManager.Instance.Mute(isMute);
        }

        #endregion

        #region Info Button
        
        /// <summary>
        /// Show information
        /// </summary>
        public void ShowInfo()
        {
            ButtonClickAudio();
            
            StartCoroutine(SwipeEffect.SwipeUp(infoPanelTransform,
                Vector3.zero, 100f));
            StartCoroutine(FadingEffect.FadeIn(infoCanvasGroup));
        }
        
        /// <summary>
        /// Hide information
        /// </summary>
        public void HideInfo(bool playAudio)
        {
            if(playAudio)
                ButtonClickAudio();
            
            StartCoroutine(SwipeEffect.SwipeDown(infoPanelTransform,
                2100 * Vector3.down, 100f));
            StartCoroutine(FadingEffect.FadeOut(infoCanvasGroup));
        }

        #endregion
    }
}