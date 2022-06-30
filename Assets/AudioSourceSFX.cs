using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceSFX : MonoBehaviour
{
    public static AudioSourceSFX instance;
    public Sound.SoundSFX[] SFXSounds;

    public void Start()
    {
        foreach (Sound.SoundSFX s in SFXSounds)
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
