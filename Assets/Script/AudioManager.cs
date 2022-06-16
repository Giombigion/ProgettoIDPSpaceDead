using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip[] clip;

    //[HideInInspector]
    public List<AudioSource> audio_source = new List<AudioSource>();

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }


    /// <summary>
    /// Pooling AUDIO (Basic)
    /// </summary>
    void Start()
    {
        for (int n = 0; n < clip.Length; n++)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip[n];
            audio_source.Add(audioSource);
        }
    }

    float clipLenght(int channel)
    {
        return audio_source[channel].clip.length;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="channel">ID CLIP</param>
    /// <param name="v">Volume (0-1)</param>
    /// <param name="spatial">SpatialBlend (0-1) 0=2D - 1=3D</param>
    /// <param name="isLoop">True/False</param>
    public void _playAudio(int channel, float v, int spatial, bool isLoop)
    {
        bool isPlay = false;
        Coroutine coroutine = null;

        if (!isPlay && coroutine == null)
        {
            audio_source[channel].Play();
            audio_source[channel].volume = v;
            audio_source[channel].loop = isLoop;
            audio_source[channel].spatialBlend = spatial;
            coroutine = StartCoroutine(StopAudio(channel, audio_source[channel].clip.length));
            isPlay = true;
        }

        coroutine = null;
    }

    IEnumerator StopAudio(int channel, float delay)
    {
        yield return new WaitForSeconds(delay);
        print("Fine Audio: " + channel);
        audio_source[channel].Stop();
        //isPlay = false;
        //coroutine = null;
    }
}
