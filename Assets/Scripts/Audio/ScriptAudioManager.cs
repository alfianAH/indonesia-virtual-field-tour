using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Audio
{
    public class ScriptAudioManager : MonoBehaviour
    {
        #region Singleton

        private static ScriptAudioManager instance;
        private const string LOG = nameof(ScriptAudioManager);
        
        /// <summary>
        /// Singleton method
        /// </summary>
        public static ScriptAudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    // Find instance
                    instance = FindObjectOfType<ScriptAudioManager>();
                    
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
        
        [ArrayElementTitle("listScriptSound")]
        public ScriptSound[] scriptSounds;
        
        private readonly Dictionary<AudioSource, bool> pauseStates = 
            new Dictionary<AudioSource, bool>();

        private void Awake()
        {
            SetInstance();

            foreach (ScriptSound scriptSound in scriptSounds)
            {
                // Make new Audio Source for each sound
                scriptSound.source = gameObject.AddComponent<AudioSource>();
                
                // Set Audio Source's properties
                scriptSound.source.playOnAwake = false;
                scriptSound.source.clip = scriptSound.clip;
                scriptSound.source.loop = scriptSound.loop;
                scriptSound.source.pitch = scriptSound.pitch;
                scriptSound.source.volume = scriptSound.volume;
                
                pauseStates.Add(scriptSound.source, false);
            }
        }
        
        /// <summary>
        /// Play audio
        /// </summary>
        /// <param name="listScriptSound">Type of sound that want to be played</param>
        public void Play(ListScriptSound listScriptSound)
        {
            GetAudioSource(listScriptSound).Play();
        }
        
        /// <summary>
        /// Stop playing audio
        /// </summary>
        /// <param name="listScriptSound">Type of sound that want to be played</param>
        public void Stop(ListScriptSound listScriptSound)
        {
            AudioSource currentAudioSource = GetAudioSource(listScriptSound);
            // If current audio source is playing, stop it
            if(currentAudioSource.isPlaying)
                currentAudioSource.Stop();
        }
        
        /// <summary>
        /// Stop all audio sources in dialogue sound
        /// </summary>
        public void StopAll()
        {
            foreach (ScriptSound sound in scriptSounds)
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
            // Mute all script audio sources
            foreach (ScriptSound scriptSound in scriptSounds)
            {
                scriptSound.source.mute = isMute;
            }
        }
        
        /// <summary>
        /// Get audio source for the right listSound
        /// </summary>
        /// <param name="listScriptSound">Type of sound that want to be played</param>
        /// <returns>Returns listSound's audio source</returns>
        private AudioSource GetAudioSource(ListScriptSound listScriptSound)
        {
            ScriptSound s = Array.Find(scriptSounds, sound => sound.listScriptSound == listScriptSound);

            if (s == null)
            {
                Debug.LogError($"ScriptSound: {listScriptSound} not found!");
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
            ScriptSound s = Array.Find(scriptSounds, 
                sound => sound.source.isPlaying == isPlaying);

            return s?.source;
        }
    }
}
