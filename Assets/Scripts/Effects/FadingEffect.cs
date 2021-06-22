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
        /// Fade effect by using canvas group alpha
        /// Increase alpha to make it visible.
        /// Decrease alpha to make it invisible.
        /// </summary>
        /// <param name="canvasGroup">CanvasGroup component</param>
        /// <param name="beforeEffect">Action before fade effect</param>
        /// <param name="afterEffect">Action after fade effect</param>
        /// <returns>Wait for certain seconds</returns>
        public static IEnumerator Fade(CanvasGroup canvasGroup, 
            Action beforeEffect = null, Action afterEffect = null)
        {
            float oldAlpha = canvasGroup.alpha;
            float newAlpha = oldAlpha <= 0.0f ? 1.0f : 0.0f;
            
            beforeEffect?.Invoke();

            // If old alpha (0) is less than new alpha (1),
            // increase old alpha to new alpha
            if (oldAlpha < newAlpha)
            {
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
            }
            // Else, decrease old alpha (1) to new alpha (0)
            else
            {
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
            }
            
            afterEffect?.Invoke();
        }
    }
}