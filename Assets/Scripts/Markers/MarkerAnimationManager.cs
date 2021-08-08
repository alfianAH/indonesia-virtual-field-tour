using UnityEngine;

namespace Markers
{
    public class MarkerAnimationManager : MonoBehaviour
    {
        #region Singleton

        private static MarkerAnimationManager instance;
        private const string LOG = nameof(MarkerAnimationManager);
        
        /// <summary>
        /// Singleton method
        /// </summary>
        public static MarkerAnimationManager Instance
        {
            get
            {
                if (instance == null)
                {
                    // Find instance
                    instance = FindObjectOfType<MarkerAnimationManager>();
                    
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
        
        [SerializeField] private Animator rotationAnimator;
        private static readonly int Play = Animator.StringToHash("play");
        private static readonly int Stop = Animator.StringToHash("stop");

        /// <summary>
        /// Actons when image target detected
        /// </summary>
        public void OnImageTargetDetected()
        {
            rotationAnimator.SetTrigger(Play);
        }

        /// <summary>
        /// Actions when image target lost
        /// </summary>
        public void OnImageTargetLost()
        {
            rotationAnimator.SetTrigger(Stop);
        }
    }
}
