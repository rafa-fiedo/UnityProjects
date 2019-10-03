using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    AudioSource _audioSource;
    public static float[] _samples = new float[512];
    public static float[] _freqBand = new float[8];
    public static float[] _freqDecreasing = new float[8];

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    private void MakeFrequencyBands()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
                sampleCount += 2; // adding 2 last samples

            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }

            average /= count;
            average *= 10;

            if(average < _freqBand[i])
            {
                _freqBand[i] -= 0.04f;
            }
            else
            {
                _freqBand[i] += 0.09f;
            }
            
        }

        // EXPLENATION
        /*
         * 22050 / 512 = 43hertz per sample
         * 
         * 20-60 Hz
         * 60-250 Hz
         * 250-500 Hz
         * 500-2000 Hz
         * 2000-4000 Hz
         * 4000-6000 Hz
         * 6000-20000 Hz
         * 
         * 0 -> 2 = 86 Hz
         * 1 -> 4 = 172 Hz - 87-258
         * 2 -> 8 = 344 Hz - 259-602
         * 3 -> 16 = 688 Hz - 603-1290
         * 4 -> 32 = 1376 Hz - 1291-2666
         * 5 -> 64 = 2752 Hz - 2667-5418
         * 6 ->128 = 5504 Hz - 5419-10922
         * 7 ->256 = 11008 Hz - 10923-21930
         * 510
         */

        // Little function that's representance numbers above

        //double hzPerSample = 43;
        //double current_hz = 0;
        //for (int i = 1; i < 9; i++)
        //{
        //    double math_pow = Math.Pow(2, i);
        //    double hertz = math_pow * hzPerSample;
        //    Console.WriteLine((i - 1) + " " + hertz + " - " + (current_hz + 1) + "-" + (current_hz + hertz));
        //    current_hz += hertz;
        //}



    }
}
