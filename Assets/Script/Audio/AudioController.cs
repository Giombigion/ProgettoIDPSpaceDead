using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public AudioSourceMusic sourceMusic;
    public AudioSourcePlayer sourcePlayer;
    public AudioSourceBckgroundSFX sourceBckgroundSFX;
    public AudioSourceSFX sourceSFX;

    //public AudioSource[] audioSources;

    float t;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (GameController.instance.idlevel == 0)
        {
            PlayMusic("Musica_Terra");
        }
        else if(GameController.instance.idlevel == 1)
        {
            PlayMusic("Musica_Nave");
        }
    }

    //-----MUSICA------------------------------------------------------------------------------------------------------
    public void PlayMusic(string name)
    {
        Sound.Music s = Array.Find(sourceMusic.MusicSounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void StopMusic(string name)
    {
        Sound.Music s = Array.Find(sourceMusic.MusicSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not stop!");
            return;
        }
        s.source.Stop();
    }

    //-----PLAYER------------------------------------------------------------------------------------------------------
    public void PlayPlayerSound(string name)
    {
        Sound.SoundPlayer s = Array.Find(sourcePlayer.PlayerSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void StopPlayerSound(string name)
    {
        Sound.SoundPlayer s = Array.Find(sourcePlayer.PlayerSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not stop!");
            return;
        }
        s.source.Stop();
    }

    //-----BCKGROUND SFX------------------------------------------------------------------------------------------------------
    public void PlayBckgroundSFX(string name)
    {
        Sound.SoundBckgroundSFX s = Array.Find(sourceBckgroundSFX.BckgroundSFXSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void StopBckgroundSFX(string name)
    {
        Sound.SoundBckgroundSFX s = Array.Find(sourceBckgroundSFX.BckgroundSFXSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not stop!");
            return;
        }
        s.source.Stop();
    }

    //-----SFX------------------------------------------------------------------------------------------------------
    public void PlaySFX(string name)
    {
        Sound.SoundSFX s = Array.Find(sourceSFX.SFXSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void StopSFX(string name)
    {
        Sound.SoundSFX s = Array.Find(sourceSFX.SFXSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not stop!");
            return;
        }
        s.source.Stop();
    }

    //-----ALTRI METODI------------------------------------------------------------------------------------------------------
    public void AudioTimer(float everyTIme, string audioname)
    {
        t += Time.deltaTime;
        if (t > everyTIme)
        {
            t = 0;
            PlayMusic(audioname);
        }
    }
}
