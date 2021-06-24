using System;
using Audio;
using Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Markers
{
    public class MarkerManager : MonoBehaviour
    {
        public MarkerDetails markerDetails;

        [Header("Detection Info Text")] 
        [SerializeField] private Animator detectionInfoAnimator;
        [SerializeField] private Text detectionInfo;
        [SerializeField] private Color detectedColor,
            undetectedColor;

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
            detectionInfo.text = $"{markerDetails.name} terdeteksi";
            
            // Change text color
            currentColor = detectionInfo.color;
            StartCoroutine(ChangeColorEffect.ChangeTextColor(detectionInfo, currentColor, detectedColor));
            
            // Play audio
            AudioManager.Instance.Play(markerDetails.sound);
        }
        
        /// <summary>
        /// Action on image target lost
        /// </summary>
        public void OnImageTargetLost()
        {
            // Set boolean in animator
            detectionInfoAnimator.SetBool(IsDetected, false);

            // Change text
            detectionInfo.text = "Target belum ditemukan";
            
            // Change text color
            currentColor = detectionInfo.color;
            StartCoroutine(ChangeColorEffect.ChangeTextColor(detectionInfo, currentColor, undetectedColor));
        }
    }
    
    [Serializable]
    public class MarkerDetails
    {
        public string name;
        [TextArea(3, 5)]
        public string info;
        public ListSound sound;
    }
}
