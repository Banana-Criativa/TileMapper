
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
    [Range(1, 20000)]  //Creates a slider in the inspector
    public float frequency1;

    [Range(1, 20000)]  //Creates a slider in the inspector
    public float frequency2;

    [Range(-1f, 1f)]
    public float offset;

    public float sampleRate = 44100;
    public float waveLengthInSeconds = 2.0f;

    //public float cutOff_On = 800.0f;
    //public float cutOff_Off = 100.0f;

    //public bool engineOn;
    public int modes;

    AudioSource audioSource;
    //AudioLowPassFilter lowPassFilter;
    int timeIndex = 0;

    void Awake()
    {
        //lowPassFilter = GetComponent<AudioLowPassFilter>();
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
        //lowPassFilter.cutoffFrequency = engineOn ? cutOff_On : cutOff_Off;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!audioSource.isPlaying)
            {
                timeIndex = 0;  //resets timer before playing sound
                audioSource.Play();
                //engineOn = true;
            }
            else
            {
                audioSource.Stop();
                //engineOn = false;
            }
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i += channels)
        {
            //data[i] = CreateSine(timeIndex, frequency1, sampleRate);
            //data[i] = CreateSumSine(timeIndex, frequency1, sampleRate, modes);
            data[i] = CreateSquare(timeIndex, frequency1, sampleRate);
            print(data[i]);

            if (channels == 2)
            {
                //data[i + 1] = CreateSine(timeIndex, frequency2, sampleRate);
                //data[i + 1] = CreateSumSine(timeIndex, frequency2, sampleRate, modes);
                data[i + 1] = CreateSquare(timeIndex, frequency2, sampleRate);
                print(data[i + 1]);
            }

            timeIndex++;

            //if timeIndex gets too big, reset it to 0
            if (timeIndex >= (sampleRate * waveLengthInSeconds))
            {
                timeIndex = 0;
            }
        }
    }

    /// <summary>
    /// Create a Sine Wave for the Audio Tiler.
    /// </summary>
    /// <param name="timeIndex">Clock wanted.</param>
    /// <param name="frequency">Wave frequency</param>
    /// <param name="sampleRate">Usually at 44100 Khz</param>
    /// <returns>
    ///     Returns the value for a sine wave between 1 and -1
    ///     for all the sine wave peaks.
    /// </returns>
    public float CreateSine(int timeIndex, float frequency, float sampleRate)
    {
        return Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate);
    }

    /// <summary>
    /// Create a sum of sinoidal waves
    /// </summary>
    /// <param name="timeIndex">Clock wanted.</param>
    /// <param name="frequency">Wave frequency</param>
    /// <param name="sampleRate">Usually at 44100 Khz</param>
    /// <param name="k">Number of Fourier Modules added to the series.</param>
    /// <returns>
    ///     It adds waves into a constructive interference. if k is big enough,
    ///     it's able to create a Square wave.
    /// </returns>
    public float CreateSumSine(int timeIndex, float frequency, float sampleRate, int k)
    {
        float intensity = 0;

        for (int i = 0; i < k; ++i)
            intensity += (Mathf.Sin((2 * k) * Mathf.PI * timeIndex * frequency / sampleRate) / k);

        return intensity;
    }

    /// <summary>
    /// Create a Square wave for the Audio Tiler.
    /// </summary>
    /// <param name="timeIndex">Clock wanted.</param>
    /// <param name="frequency">Wave frequency</param>
    /// <param name="sampleRate">Usually at 44100 Khz</param>
    /// <returns>
    ///     Returns the wave peak at the square wave
    ///     which can return 1 or -1 for the wave peak.
    /// </returns>
    public float CreateSquare(int timeIndex, float frequency, float sampleRate)
    {
        if (Mathf.Floor(timeIndex * frequency / sampleRate) % 2 == 0) return 1f;
        else return -1f;
    }


    /// <summary>
    /// Used for creating white noise.
    /// Will adapt it for the current code
    /// </summary>
    /*

    System.Random rand = new System.Random();

    void Awake()
    {
        
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

