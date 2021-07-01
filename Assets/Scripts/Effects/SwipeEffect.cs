using System.Collections;
using UnityEngine;

namespace Effects
{
    public static class SwipeEffect
    {
        /// <summary>
        /// Swipe down rect transform
        /// </summary>
        /// <param name="rectTransform">Rect Transform</param>
        /// <param name="targetPos">Target position</param>
        /// <param name="velocity">Swipe velocity</param>
        /// <param name="waitingTime">Waiting time every movement</param>
        /// <returns>Wait for waitingTime</returns>
        public static IEnumerator SwipeDown(RectTransform rectTransform, 
            Vector3 targetPos, float velocity, float waitingTime = 0.01f)
        {
            // If current y-position is still above the target position, ...
            while (rectTransform.localPosition.y > targetPos.y)
            {
                // Decrease the current position
                rectTransform.localPosition += velocity * Vector3.down;
                // Wait
                yield return new WaitForSeconds(waitingTime);
            }
        }
        
        /// <summary>
        /// Swipe up rect transform
        /// </summary>
        /// <param name="rectTransform">Rect Transform</param>
        /// <param name="targetPos">Target position</param>
        /// <param name="velocity">Swipe velocity</param>
        /// <param name="waitingTime">Waiting time every movement</param>
        /// <returns>Wait for waitingTime</returns>
        public static IEnumerator SwipeUp(RectTransform rectTransform, 
            Vector3 targetPos, float velocity, float waitingTime = 0.01f)
        {
            // If current y-position is still below the target position, ...
            // Note: Round the current position to get exact 0
            while (Mathf.Round(rectTransform.localPosition.y) < targetPos.y)
            {
                // Increase the position
                rectTransform.localPosition += velocity * Vector3.up;
                // Wait
                yield return new WaitForSeconds(waitingTime);
            }
        }
    }
}