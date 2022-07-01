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
            PlayMusic("Musica_Terra");

            Debug.Log("Suono la musica della terra");
        }
        else if(GameController.instance.idlevel == 1)
        {
            PlayMusic("Musica_Nave");

            Debug.Log("Suono la musica della nave");
        }
    }

    //-----MUSIC-------------------------------------------------------------------------------------------------------------------
    public void PlayMusic(string name)
    {
        Sound.Music s = Array.Find(sourceMusic.musics, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        sourceMusic.SetMusic(name);
        s.source.Play();
    }

    public void StopMusic(string name)
    {
        Sound.Music s = Array.Find(sourceMusic.musics, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not stop!");
            return;
        }
        sourceMusic.SetMusic(name);
        s.source.Stop();
    }

    //-----PLAYER-------------------------------------------------------------------------------------------------------------------
    public void PlayPlayerSound(string name)
    {
        Sound.PlayerSound s = Array.Find(sourcePlayer.playerSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void StopPlayerSound(string name)
    {
        Sound.PlayerSound s = Array.Find(sourcePlayer.playerSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not stop!");
            return;
        }
        s.source.Stop();
    }

    //-----BCKGROUND SFX-------------------------------------------------------------------------------------------------------------------
    public void PlayBckgroundSFX(string name)
    {
        Sound.BckgroundSFX s = Array.Find(sourceBckgroundSFX.bckgroundSFX, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void StopBckgroundSFX(string name)
    {
        Sound.BckgroundSFX s = Array.Find(sourceBckgroundSFX.bckgroundSFX, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not stop!");
            return;
        }
        s.source.Stop();
    }

    //-----SFX-------------------------------------------------------------------------------------------------------------------
    public void PlaySFX(string name)
    {
        Sound.SFX s = Array.Find(sourceSFX.SFXes, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        sourceSFX.SetSFX(name);
        s.source.Play();
    }

    public void StopSFX(string name)
    {
        Sound.SFX s = Array.Find(sourceSFX.SFXes, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not stop!");
            return;
        }
        sourceSFX.SetSFX(name);
        s.source.Stop();
    }

    //-----ALTRI METODI-------------------------------------------------------------------------------------------------------
    public void AudioTimer(float everyTIme, string audioname)
    {
        t += Time.deltaTime;
        if (t > everyTIme)
        {
            t = 0;
            //Play(audioname);
        }
    }
}
