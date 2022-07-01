using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceScript : MonoBehaviour
{
    public static AudioSourceScript instance;
    public Sound[] sounds;

    // Start is called before the first frame update
    public void SetSound(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                s.source = GetComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.panStereo = s.pan;
                s.source.loop = s.loop;
                s.source.playOnAwake = s.playOnAwake;
                s.source.mute = s.mute;
                s.source.outputAudioMixerGroup = s.output;
            }
        }
    }
}
