using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public AudioSourceScript sourceMusic;
    public AudioSourceScript sourcePlayer;
    public AudioSourceScript sourceBckgroundSFX;
    public AudioSourceScript sourceSFX;

    float t;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if(GameController.instance.idlevel == 0)
        {
            PlaySound(sourceMusic, "Musica_Terra");
            PlaySound(sourceBckgroundSFX, "Rain_Terra");
        }
        else if(GameController.instance.idlevel == 1)
        {
            StopSound(sourceMusic, "Musica_Nave");
        }
    }

    //-----PLAY AND STOP-------------------------------------------------------------------------------------------------------------------
    public void PlaySound(AudioSourceScript AudioSource, string name)
    {
        Sound s = Array.Find(AudioSource.sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        AudioSource.SetSound(name);
        s.source.Play();
    }

    public void StopSound(AudioSourceScript AudioSource, string name)
    {
        Sound s = Array.Find(AudioSource.sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not stop!");
            return;
        }
        AudioSource.SetSound(name);
        s.source.Stop();
    }

    //-----ALTRI METODI-------------------------------------------------------------------------------------------------------
    public void AudioTimer(float everyTIme, string audioname)
    {
        t += Time.deltaTime;
        if (t > everyTIme)
        {
            t = 0;
            PlaySound(sourcePlayer ,audioname);
        }
    }
}
