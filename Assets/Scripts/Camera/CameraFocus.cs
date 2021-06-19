using UnityEngine;
using Vuforia;

namespace Camera
{
    public class CameraFocus : MonoBehaviour
    {
        private void Start()
        {
            var vuforia = VuforiaARController.Instance;
            
            vuforia.RegisterVuforiaStartedCallback(OnVuforiaStarted);
            vuforia.RegisterOnPauseCallback(OnPaused);
        }
        
        /// <summary>
        /// On Vuforia started, set camera focus mode
        /// </summary>
        private void OnVuforiaStarted()
        {
            CameraDevice.Instance.SetFocusMode(
                CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
        
        /// <summary>
        /// On paused
        /// </summary>
        /// <param name="paused"></param>
        private void OnPaused(bool paused)
        {
            // When resumed, ...
            if (!paused)
            {
                // Set again auto focus mode when app is resumed
                CameraDevice.Instance.SetFocusMode(
                    CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
            }
        }
    }
}
