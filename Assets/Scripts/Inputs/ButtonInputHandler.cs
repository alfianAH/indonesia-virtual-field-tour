using Effects;
using UnityEngine;

namespace Inputs
{
    public class ButtonInputHandler : MonoBehaviour
    {
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
    }
}