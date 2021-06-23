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

        [Header("Detection Info Text")] 
        [SerializeField] private Animator detectionInfoAnimator;
        [SerializeField] private Text detectionInfo;
        [SerializeField] private Color detectedColor,
            undetectedColor;
        [SerializeField] private float colorIntervalTime = 1.5f;

        private Color currentColor;
        private static readonly int IsDetected = Animator.StringToHash("isDetected");

        /// <summary>
        /// Action on image target detected
        /// </summary>
        public void OnImageTargetDetected()
        {
            Debug.Log("Target Detected");
            // Set boolean in animator
            detectionInfoAnimator.SetBool(IsDetected, true);
            
            // Change text
            detectionInfo.text = $"{imageTargetDetails.name} terdeteksi";
            
            // Change text color
            currentColor = detectionInfo.color;
            StartCoroutine(ChangeDetectionInfoColor(currentColor, detectedColor));
            
            // Play audio
            AudioManager.Instance.Play(imageTargetDetails.sound);
        }
        
        /// <summary>
        /// Action on image target lost
        /// </summary>
        public void OnImageTargetLost()
        {
            Debug.Log("Target Lost");
            // Set boolean in animator
            detectionInfoAnimator.SetBool(IsDetected, false);

            // Change text
            detectionInfo.text = "Target belum ditemukan";
            
            // Change text color
            currentColor = detectionInfo.color;
            StartCoroutine(ChangeDetectionInfoColor(currentColor, undetectedColor));
        }
        
        /// <summary>
        /// Change detection info text's color
        /// </summary>
        /// <param name="oldColor"></param>
        /// <param name="newColor"></param>
        /// <returns></returns>
        private IEnumerator ChangeDetectionInfoColor(Color oldColor, Color newColor)
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
        [TextArea(3, 5)]
        public string info;
        public ListSound sound;
    }
}
