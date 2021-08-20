using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Waves : MonoBehaviour
{
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
}