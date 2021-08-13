using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource _source;

    [Range(-1f, 1f)]
    public float offset;

    public float cutOff_On = 800.0f;
    public float cutOff_Off = 100.0f;

    public bool engineOn;

    System.Random rand = new System.Random();
    AudioLowPassFilter lowPassFilter;

    void Awake()
    {
        lowPassFilter = GetComponent<AudioLowPassFilter>();
        Update();
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = (float)(rand.NextDouble() * 2.0 - 1.0 + offset);
        }
    }

    void Update()
    {
        lowPassFilter.cutoffFrequency = engineOn ? cutOff_On : cutOff_Off;
    }
}
