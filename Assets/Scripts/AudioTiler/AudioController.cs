
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Websites used for reference:
/// Sine waves Reference - https://forum.unity.com/threads/generating-a-simple-sinewave.471529/
/// Gamasutra Procedural Audio - https://www.gamasutra.com/blogs/JoeStrout/20170223/292317/Procedural_Audio_in_Unity.php 
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    /*
    private AudioSource _source;

    [Range(-1f, 1f)]
    public float offset;

    public float cutOff_On = 800.0f;
    public float cutOff_Off = 100.0f;

    public bool engineOn;

    System.Random rand = new System.Random();
    AudioLowPassFilter lowPassFilter;
    */

    [Range(1, 20000)]  //Creates a slider in the inspector
    public float frequency1;

    [Range(1, 20000)]  //Creates a slider in the inspector
    public float frequency2;

    public float sampleRate = 44100;
    public float waveLengthInSeconds = 2.0f;

    AudioSource audioSource;
    int timeIndex = 0;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0; //force 2D sound
        audioSource.Stop(); //avoids audiosource from starting to play automatically
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!audioSource.isPlaying)
            {
                timeIndex = 0;  //resets timer before playing sound
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i += channels)
        {
            data[i] = CreateSine(timeIndex, frequency1, sampleRate);

            if (channels == 2)
                data[i + 1] = CreateSine(timeIndex, frequency2, sampleRate);

            timeIndex++;

            //if timeIndex gets too big, reset it to 0
            if (timeIndex >= (sampleRate * waveLengthInSeconds))
            {
                timeIndex = 0;
            }
        }
    }

    //Creates a sinewave
    public float CreateSine(int timeIndex, float frequency, float sampleRate)
    {
        return Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate);
    }


    /// <summary>
    /// Used for creating white noise.
    /// Will adapt it for the current code
    /// </summary>
    /*
    void Awake()
    {
        lowPassFilter = GetComponent<AudioLowPassFilter>();
        Update();
    } 

    void OnAudioFilterRead(float[] data, int channels)
    {
        //Debug.Log("Available audio channels: " + data.Length.ToString() + "\nChannels used: " + channels.ToString());
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = (float)(rand.NextDouble() * 2.0 - 1.0 + offset);
        }
    }

    void Update()
    {
        lowPassFilter.cutoffFrequency = engineOn ? cutOff_On : cutOff_Off;
    } */
}

