using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceBckgroundSFX : MonoBehaviour
{
    public static AudioSourceBckgroundSFX instance;
    public Sound.BckgroundSFX[] bckgroundSFX;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Sound.BckgroundSFX s in bckgroundSFX)
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
