using System;
using UnityEngine;

namespace Audio
{
    [Serializable]
    public class Sound
    {
        public AudioClip clip;

        [Range(0, 1)] public float volume = 1;
        [Range(-3, 3)] public float pitch = 1;

        public bool loop;
        
        [HideInInspector]
        public AudioSource source;
    }
    
    [Serializable]
    public class ScriptSound: Sound
    {
        public ListScriptSound listScriptSound;
    }
    
    [Serializable]
    public class EffectSound: Sound
    {
        public ListEffectSound listEffectSound;
    }
}