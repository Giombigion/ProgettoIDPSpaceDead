using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePlayer : MonoBehaviour
{
    public static AudioSourcePlayer instance;
    public Sound.SoundPlayer[] PlayerSounds;
    public void Start()
    {
        foreach (Sound.SoundPlayer s in PlayerSounds)
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
