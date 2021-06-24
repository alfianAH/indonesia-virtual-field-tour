using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Effects
{
    public static class ChangeColorEffect
    {
        /// <summary>
        /// Change targetText's color
        /// </summary>
        /// <param name="targetText">Target text</param>
        /// <param name="oldColor">Target text's old color</param>
        /// <param name="newColor">Target text's new color</param>
        /// <param name="colorIntervalTime">Interval time to change color. Default is 1.5s</param>
        /// <returns></returns>
        public static IEnumerator ChangeTextColor(Text targetText, 
            Color oldColor, Color newColor, 
            float colorIntervalTime = 1.5f)
        {
            float startTime = 0.0f;

            while (startTime < colorIntervalTime)
            {
                targetText.color = Color.Lerp(oldColor, newColor, 
                    startTime / colorIntervalTime);

                startTime += Time.deltaTime;
                
                yield return null;
            }
        }
    }
}