using System;
using Audio;
using UnityEngine;

namespace Markers
{
    public class MarkerManager : MonoBehaviour
    {
        public MarkerDetails markerDetails;

        /// <summary>
        /// Action on image target detected
        /// </summary>
        public void OnImageTargetDetected()
        { 
            // Set actions on detection info when image target is detected
            MarkerInfoManager.Instance.OnImageTargetDetected(markerDetails);
            
            // Play audio
            ScriptAudioManager.Instance.Play(markerDetails.scriptSound);
        }
        
        /// <summary>
        /// Action on image target lost
        /// </summary>
        public void OnImageTargetLost()
        {
            // Set actions on detection info when image target is lost
            MarkerInfoManager.Instance.OnImageTargetLost();

            // Stop playing audio
            ScriptAudioManager.Instance.Stop(markerDetails.scriptSound);
        }
    }
    
    [Serializable]
    public class MarkerDetails
    {
        public string name;
        [TextArea(3, 5)]
        public string info;
        public ListScriptSound scriptSound;
    }
}
