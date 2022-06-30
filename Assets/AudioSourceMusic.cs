using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceMusic : MonoBehaviour
{
    public static AudioSourceMusic instance;
    public Sound.Music[] MusicSounds;

    public void Start()
    {
        foreach (Sound.Music s in MusicSounds)
        {
            s.source = gameObject.GetComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            s.source.mute = s.mute;
            s.source.outputAudioMixerGroup = s.output;
        }
    }
}
