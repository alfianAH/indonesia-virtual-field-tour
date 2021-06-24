using System;
using System.Collections;
using UnityEngine;

namespace Effects
{
    public static class FadingEffect
    {
        private const float FADE_WAITING_TIME = 0.05f;
        private const float FADING_SPEED = 0.1f;

        /// <summary>
        /// Fade in effect by using canvas group alpha
        /// From invisible (0) to visible (1)
        /// </summary>
        /// <param name="canvasGroup">CanvasGroup component</param>
        /// <param name="beforeEffect">Action before fade effect</param>
        /// <param name="afterEffect">Action after fade effect</param>
        /// <returns>Wait for certain seconds</returns>
        public static IEnumerator FadeIn(CanvasGroup canvasGroup, 
            Action beforeEffect = null, Action afterEffect = null)
        {
            float oldAlpha = 0;
            float newAlpha = 1;
            
            beforeEffect?.Invoke();
            
            while (oldAlpha < newAlpha)
            {
                float currentAlpha = canvasGroup.alpha;
                
                // Increase current alpha
                currentAlpha += FADING_SPEED;
                canvasGroup.alpha = currentAlpha;
                
                // Update old alpha
                oldAlpha = currentAlpha;
                yield return new WaitForSeconds(FADE_WAITING_TIME);
            }
            // Block the raycast
            canvasGroup.blocksRaycasts = true;

            afterEffect?.Invoke();
        }
        
        /// <summary>
        /// Fade out effect by using canvas group alpha
        /// From visible (1) to invisible (0)
        /// </summary>
        /// <param name="canvasGroup">CanvasGroup component</param>
        /// <param name="beforeEffect">Action before fade effect</param>
        /// <param name="afterEffect">Action after fade effect</param>
        /// <returns>Wait for certain seconds</returns>
        public static IEnumerator FadeOut(CanvasGroup canvasGroup, 
            Action beforeEffect = null, Action afterEffect = null)
        {
            float oldAlpha = 1;
            float newAlpha = 0;
            
            beforeEffect?.Invoke();
            
            while (oldAlpha > newAlpha)
            {
                float currentAlpha = canvasGroup.alpha;
                
                // Decrease current alpha
                currentAlpha -= FADING_SPEED;
                canvasGroup.alpha = currentAlpha;
                
                // Update old alpha
                oldAlpha = currentAlpha;
                yield return new WaitForSeconds(FADE_WAITING_TIME);
            }
            // Don't block the raycast
            canvasGroup.blocksRaycasts = false;

            afterEffect?.Invoke();
        }
    }
}