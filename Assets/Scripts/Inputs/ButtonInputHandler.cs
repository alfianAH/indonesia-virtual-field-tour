using Effects;
using UnityEngine;

namespace Inputs
{
    public class ButtonInputHandler : MonoBehaviour
    {
        #region Pause and Resume Buttons

        /// <summary>
        /// Pause the game 
        /// </summary>
        /// <param name="pauseCanvasGroup">Pause Canvas Group</param>
        public void PauseGame(CanvasGroup pauseCanvasGroup)
        {
            StartCoroutine(FadingEffect.Fade(pauseCanvasGroup, 
                afterEffect:() => Time.timeScale = 0f)
            );
        }
        
        /// <summary>
        /// Resume the game
        /// </summary>
        /// <param name="pauseCanvasGroup">Pause Canvas Group</param>
        public void ResumeGame(CanvasGroup pauseCanvasGroup)
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
        /// <param name="infoCanvasGroup">Information Canvas Group</param>
        public void ShowInfo(CanvasGroup infoCanvasGroup)
        {
            StartCoroutine(FadingEffect.Fade(infoCanvasGroup));
        }
        
        /// <summary>
        /// Hide information
        /// </summary>
        /// <param name="infoCanvasGroup">Information Canvas Group</param>
        public void HideInfo(CanvasGroup infoCanvasGroup)
        {
            StartCoroutine(FadingEffect.Fade(infoCanvasGroup));
        }

        #endregion
    }
}