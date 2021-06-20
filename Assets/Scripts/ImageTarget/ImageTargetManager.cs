using System;
using System.Collections;
using Audio;
using UnityEngine;
using UnityEngine.UI;

namespace ImageTarget
{
    public class ImageTargetManager : MonoBehaviour
    {
        public ImageTargetDetails imageTargetDetails;

        [SerializeField] private Text detectionInfo;
        [SerializeField] private Color detectedColor,
            undetectedColor;
        [SerializeField] private float colorIntervalTime = 1.5f;
        private Color currentColor;

        /// <summary>
        /// Action on image target detected
        /// </summary>
        public void OnImageTargetDetected()
        {
            Debug.Log("Target Detected");
            // Change text
            detectionInfo.text = $"{imageTargetDetails.name} terdeteksi";
            
            // Change text color
            currentColor = detectionInfo.color;
            StartCoroutine(ChangeTextColor(currentColor, detectedColor));
            
            // Play audio
            AudioManager.Instance.Play(imageTargetDetails.sound);
        }
        
        /// <summary>
        /// Action on image target lost
        /// </summary>
        public void OnImageTargetLost()
        {
            Debug.Log("Target Lost");
            // Change text
            detectionInfo.text = "Target belum ditemukan";
            // Change text color
            currentColor = detectionInfo.color;
            StartCoroutine(ChangeTextColor(currentColor, undetectedColor));
        }
        
        /// <summary>
        /// Change detection info text's color
        /// </summary>
        /// <param name="oldColor"></param>
        /// <param name="newColor"></param>
        /// <returns></returns>
        private IEnumerator ChangeTextColor(Color oldColor, Color newColor)
        {
            float startTime = 0.0f;

            while (startTime < colorIntervalTime)
            {
                detectionInfo.color = Color.Lerp(oldColor, newColor, 
                    startTime / colorIntervalTime);

                startTime += Time.deltaTime;
                
                yield return null;
            }
        }
    }
    
    [Serializable]
    public class ImageTargetDetails
    {
        public string name;
        public ListSound sound;
    }
}
