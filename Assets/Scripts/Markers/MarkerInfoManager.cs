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

        [SerializeField] private Button infoButton;
        [SerializeField] private Text markerTitle;
        [SerializeField] private Text markerInfo;

        /// <summary>
        /// Set marker info text
        /// Marker info is set when marker is detected
        /// </summary>
        /// <param name="title"></param>
        /// <param name="infoText"></param>
        public void SetMarkerInfo(string title, string infoText)
        {
            infoButton.interactable = true;
            markerTitle.text = title;
            markerInfo.text = infoText;
        }
        
        /// <summary>
        /// Unset marker info
        /// To prevent user open info when marker is not detected
        /// </summary>
        public void UnsetMarkerInfo()
        {
            infoButton.interactable = false;
            ButtonInputHandler.Instance.HideInfo();
        }
    }
}
