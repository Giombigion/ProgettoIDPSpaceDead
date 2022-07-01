using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    [Range(-1f, 1f)]
    public float pan;

    public bool loop;
    public bool playOnAwake;
    public bool mute;

    public AudioMixerGroup output;

    public AudioSource source;
}
