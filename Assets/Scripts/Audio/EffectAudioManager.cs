using System;
using UnityEngine;

namespace Audio
{
    public class EffectAudioManager : MonoBehaviour
    {
        #region Singleton

        private static EffectAudioManager instance;
        private const string LOG = nameof(EffectAudioManager);
        
        /// <summary>
        /// Singleton method
        /// </summary>
        public static EffectAudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    // Find instance
                    instance = FindObjectOfType<EffectAudioManager>();
                    
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

        #region Don't Destroy On Load
        
        /// <summary>
        /// Use only 1 Audio manager
        /// </summary>
        private void SetInstance()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        

        #endregion

        [ArrayElementTitle("listEffectSound")]
        public EffectSound[] effectSounds;

        private void Awake()
        {
            SetInstance();

            foreach (EffectSound effectSound in effectSounds)
            {
                // Make new Audio Source for each sound
                effectSound.source = gameObject.AddComponent<AudioSource>();
                
                // Set Audio Source's properties
                effectSound.source.playOnAwake = false;
                effectSound.source.clip = effectSound.clip;
                effectSound.source.loop = effectSound.loop;
                effectSound.source.pitch = effectSound.pitch;
                effectSound.source.volume = effectSound.volume;
            }
        }
        
        /// <summary>
        /// Play audio
        /// </summary>
        /// <param name="listEffectSound">Type of sound that want to be played</param>
        public void Play(ListEffectSound listEffectSound)
        {
            GetAudioSource(listEffectSound).Play();
        }
        
        /// <summary>
        /// Get audio source for the right listSound
        /// </summary>
        /// <param name="listEffectSound">Type of sound that want to be played</param>
        /// <returns>Returns listSound's audio source</returns>
        private AudioSource GetAudioSource(ListEffectSound listEffectSound)
        {
            EffectSound s = Array.Find(effectSounds, sound => sound.listEffectSound == listEffectSound);

            if (s == null)
            {
                Debug.LogError($"EffectSound: {listEffectSound} not found!");
                return null;
            }

            return s.source;
        }
    }
}
