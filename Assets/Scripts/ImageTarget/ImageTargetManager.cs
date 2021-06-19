using Audio;
using UnityEngine;

namespace ImageTarget
{
    public class ImageTargetManager : MonoBehaviour
    {
        /// <summary>
        /// Action on image target detected
        /// </summary>
        public void OnImageTargetDetected()
        {
            AudioManager.Instance.Play(ListSound.Marker1);
        }
    }
}
