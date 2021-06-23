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
        N1Aceh, N2Padang, N3Palembang,
        N4TanjungPinang, N5Lampung, N6Jakarta,
        N7Bandung, N8Bogor, N9Magelang,
        N10Yogyakarta, N11Surabaya, N12Pontianak,
        N13TanjungSelor, N14Palangkaraya, N15Bulukumba,
        N16TanaToraja, N17Makassar, N18PulauKomodo,
        N19Lombok, N20Bali, N21Ambon,
        N22Papua, N23MonumenSelamatDatang, N24CandiPrambanan,
        N25MonumenSuroboyo, N26TuguSimpangLimaGumul
    }
}