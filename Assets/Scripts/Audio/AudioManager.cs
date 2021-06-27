using System;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        #region Singleton

        private static AudioManager instance;
        private const string LOG = nameof(AudioManager);
        
        /// <summary>
        /// Singleton method
        /// </summary>
        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    // Find instance
                    instance = FindObjectOfType<AudioManager>();
                    
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
        
        [ArrayElementTitle("listSound")]
        public Sound[] sounds;

        private void Awake()
        {
            SetInstance();

            foreach (Sound sound in sounds)
            {
                // Make new Audio Source for each sound
                sound.source = gameObject.AddComponent<AudioSource>();
                
                // Set Audio Source's properties
                sound.source.playOnAwake = false;
                sound.source.clip = sound.clip;
                sound.source.loop = sound.loop;
                sound.source.pitch = sound.pitch;
                sound.source.volume = sound.volume;
            }
        }
        
        /// <summary>
        /// Play audio
        /// </summary>
        /// <param name="listSound">Type of sound that want to be played</param>
        public void Play(ListSound listSound)
        {
            GetAudioSource(listSound).Play();
        }
        
        /// <summary>
        /// Stop playing audio
        /// </summary>
        /// <param name="listSound">Type of sound that want to be played</param>
        public void Stop(ListSound listSound)
        {
            AudioSource currentAudioSource = GetAudioSource(listSound);
            // If current audio source is playing, stop it
            if(currentAudioSource.isPlaying)
                currentAudioSource.Stop();
        }
        
        /// <summary>
        /// Pause audio
        /// </summary>
        public void Pause()
        {
            // Get currently playing audio source
            AudioSource currentAudioSource = GetAudioSourcePlaying(true);
            // If current audio source is playing, pause it
            if(currentAudioSource != null)
            {
                currentAudioSource.Pause();
            }
        }
        
        /// <summary>
        /// Unpause the paused audio
        /// </summary>
        public void UnPause()
        {
            // Get currently paused audio source
            AudioSource currentAudioSource = GetAudioSourcePlaying(false);
            // If current audio source is paused, unpause it
            if(currentAudioSource != null)
            {
                currentAudioSource.UnPause();
            }
        }
        
        /// <summary>
        /// Mute or Unmute audio
        /// </summary>
        /// <param name="isMute">True for mute, False for unmute</param>
        public void Mute(bool isMute)
        {
            // Get currently playing audio source
            AudioSource currentAudioSource = GetAudioSourcePlaying(true);
            // Mute or unmute audio
            if(currentAudioSource != null)
            {
                currentAudioSource.mute = isMute;
            }
        }
        
        /// <summary>
        /// Get audio source for the right listSound
        /// </summary>
        /// <param name="listSound">Type of sound that want to be played</param>
        /// <returns>Returns listSound's audio source</returns>
        private AudioSource GetAudioSource(ListSound listSound)
        {
            Sound s = Array.Find(sounds, sound => sound.listSound == listSound);

            if (s == null)
            {
                Debug.LogError($"Sound: {listSound} not found!");
                return null;
            }

            return s.source;
        }
        
        /// <summary>
        /// Get audio source that is currently playing
        /// </summary>
        /// <returns>Returns listSound's audio source</returns>
        private AudioSource GetAudioSourcePlaying(bool isPlaying)
        {
            Sound s = Array.Find(sounds, sound => sound.source.isPlaying == isPlaying);

            return s?.source;
        }
    }
}
