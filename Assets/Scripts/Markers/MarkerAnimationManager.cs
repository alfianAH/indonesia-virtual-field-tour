using UnityEngine;

namespace Markers
{
    public class MarkerAnimationManager : SingletonBaseClass<MarkerAnimationManager>
    {
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
