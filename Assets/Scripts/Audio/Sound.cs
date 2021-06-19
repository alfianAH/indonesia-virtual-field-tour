using System;
using UnityEngine;

namespace Audio
{
    [Serializable]
    public class Sound
    {
        public ListSound listSound;

        public AudioClip clip;

        [Range(0, 1)] public float volume;
        [Range(-3, 3)] public float pitch = 1;

        public bool loop;
        public AudioSource source;
    }

    public enum ListSound
    {
        Marker1,
        Marker2,
        Marker3
    }
}