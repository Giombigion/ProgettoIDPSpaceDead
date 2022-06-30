using UnityEngine.Audio;
using UnityEngine;


public class Sound
{
    [System.Serializable]
    public class Music
    {
        public string name;

        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;
        [Range(.1f, 3f)]
        public float pitch;

        public bool loop;
        public bool playOnAwake;
        public bool mute;

        public AudioMixerGroup output;

        [HideInInspector]
        public AudioSource source;
    }

    [System.Serializable]
    public class SoundPlayer
    {
        public string name;

        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;
        [Range(.1f, 3f)]
        public float pitch;

        public bool loop;
        public bool playOnAwake;
        public bool mute;

        public AudioMixerGroup output;

        [HideInInspector]
        public AudioSource source;
    }

    [System.Serializable]
    public class SoundBckgroundSFX
    {
        public string name;

        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;
        [Range(.1f, 3f)]
        public float pitch;

        public bool loop;
        public bool playOnAwake;
        public bool mute;

        public AudioMixerGroup output;

        [HideInInspector]
        public AudioSource source;
    }

    [System.Serializable]
    public class SoundSFX
    {
        public string name;

        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;
        [Range(.1f, 3f)]
        public float pitch;

        public bool loop;
        public bool playOnAwake;
        public bool mute;

        public AudioMixerGroup output;

        [HideInInspector]
        public AudioSource source;
    }
}
