using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceMusic : MonoBehaviour
{
    public static AudioSourceMusic instance;
    public Sound.Music[] musics;

    // Start is called before the first frame update
    public void SetMusic(string name)
    {
        foreach (Sound.Music s in musics)
        {
            if(s.name == name)
            {
                s.source = GetComponent<AudioSource>();
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
}
