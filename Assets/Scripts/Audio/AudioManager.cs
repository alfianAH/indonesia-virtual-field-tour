using System;
using System.Collections.Generic;
using System.Linq;
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
        
        private Dictionary<AudioSource, bool> pauseStates = new Dictionary<AudioSource, bool>();

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
                
                pauseStates.Add(sound.source, false);
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
        /// Stop all audio sources in dialogue sound
        /// </summary>
        public void StopAll()
        {
            foreach (Sound sound in sounds)
            {
                sound.source.Stop();
            }
        }
        
        /// <summary>
        /// Pause audio
        /// </summary>
        public void Pause()
        {
            // Get currently playing audio source
            AudioSource currentAudioSource = GetAudioSourcePlaying(true);
            
            // If currentAudioSource is null, return
            if (currentAudioSource == null) return;
            
            // Loop in pause state dictionary and get the same audio source
            // as currentAudioSource
            foreach (AudioSource source in pauseStates.Keys.Where(
                source => source == currentAudioSource))
            {
                // Update the pause state to true
                pauseStates[source] = true;
                // Pause the audio source
                source.Pause();
                break;
            }
        }
        
        /// <summary>
        /// Unpause the paused audio
        /// </summary>
        public void UnPause()
        {
            // Loop in pause state dictionary, and get the audio source that is paused
            foreach (AudioSource source in pauseStates.Keys.Where(
                source => pauseStates[source]))
            {
                // Unpause the audio source
                source.UnPause();
                // Update the pause state to false
                pauseStates[source] = false;
                break;
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
