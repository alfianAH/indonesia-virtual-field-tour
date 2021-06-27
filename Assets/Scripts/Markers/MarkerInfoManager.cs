using Effects;
using Inputs;
using UnityEngine;
using UnityEngine.UI;

namespace Markers
{
    public class MarkerInfoManager : MonoBehaviour
    {
        #region Singleton

        private static MarkerInfoManager instance;
        private const string LOG = nameof(MarkerInfoManager);
        
        /// <summary>
        /// Singleton method
        /// </summary>
        public static MarkerInfoManager Instance
        {
            get
            {
                if (instance == null)
                {
                    // Find instance
                    instance = FindObjectOfType<MarkerInfoManager>();
                    
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
        
        [Header("Detection Info")]
        [SerializeField] private Animator detectionInfoAnimator;
        [SerializeField] private Image detectionInfoImage;
        [SerializeField] private Text detectionInfoText;
        [SerializeField] private Color detectedColor;
        [SerializeField] private Color undetectedColor;
        private Color currentColor;
        private static readonly int IsDetected = Animator.StringToHash("isDetected");
        
        [Header("Marker Info")]
        [SerializeField] private Button infoButton;
        [SerializeField] private Text markerTitle;
        [SerializeField] private Text markerInfo;

        #region Marker Info Text

        /// <summary>
        /// Set marker info text
        /// Marker info is set when marker is detected
        /// </summary>
        /// <param name="title"></param>
        /// <param name="infoText"></param>
        private void SetMarkerInfo(string title, string infoText)
        {
            infoButton.interactable = true;
            markerTitle.text = title;
            markerInfo.text = infoText;
        }
        
        /// <summary>
        /// Unset marker info
        /// To prevent user open info when marker is not detected
        /// </summary>
        private void UnsetMarkerInfo()
        {
            infoButton.interactable = false;
            ButtonInputHandler.Instance.HideInfo();
        }

        #endregion

        #region Marker Detection Info
        
        /// <summary>
        /// Actons when image target detected
        /// </summary>
        /// <param name="markerDetails">Marker details</param>
        public void OnImageTargetDetected(MarkerDetails markerDetails)
        {
            // Set boolean in animator
            detectionInfoAnimator.SetBool(IsDetected, true);

            // Change text
            detectionInfoText.text = markerDetails.name;
            
            // Change info color
            currentColor = detectionInfoText.color;
            // Info text
            StartCoroutine(ChangeColorEffect.ChangeColor(
                detectionInfoText, currentColor, detectedColor));
            // Info image
            StartCoroutine(ChangeColorEffect.ChangeColor(
                detectionInfoImage, currentColor, detectedColor));
            
            // Change marker info text
            SetMarkerInfo(markerDetails.name, markerDetails.info);
        }
        
        /// <summary>
        /// Actions when image target lost
        /// </summary>
        public void OnImageTargetLost()
        {
            // Set boolean in animator
            detectionInfoAnimator.SetBool(IsDetected, false);

            // Change text
            detectionInfoText.text = "Arahkan ke kartu hak milik";
            
            // Change info color
            currentColor = detectionInfoText.color;
            // Info text
            StartCoroutine(ChangeColorEffect.ChangeColor(
                detectionInfoText, currentColor, undetectedColor));
            // Info image
            StartCoroutine(ChangeColorEffect.ChangeColor(
                detectionInfoImage, currentColor, undetectedColor));
            
            // Change marker info text
            UnsetMarkerInfo();
        }

        #endregion
    }
}
