using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSliderController : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetMainAudio(float volume)
    {
        mixer.SetFloat("MainVolume", volume);
    }

    public void SetMusicAudio(float volume)
    {
        mixer.SetFloat("MusicVolume", volume);
    }
}
