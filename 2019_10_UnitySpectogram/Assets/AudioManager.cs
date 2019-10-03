using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float _dbMulti = 50;

    // first value is a "sample colums" and each of column have sample data
    // it's used for making nice geometric stuff
    public static float[][] _bandVolumes;

    private AudioSource _audioSource;

    public static int SAMPLE_COUNT = 1024;
    public static int SAMPLE_COLUMS = 100;

    private List<float> _bands;

    void Start()
    {
        // creating range of bands, they should work flexible
        _bands = new List<float>()
        {
            20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000
        };


        _audioSource = GetComponent<AudioSource>();

        _bandVolumes = new float[SAMPLE_COLUMS][];

        for(int i =0; i< SAMPLE_COLUMS; i++)
        {
            _bandVolumes[i] = new float[_bands.Count - 1]; // -1 beause bands are "from,to" like from 20 to 50
        }

    }

    void Update()
    {
        // copying last values level "up"
        for(int i = SAMPLE_COLUMS - 1; i>0; i--)
        {
            Array.Copy(_bandVolumes[i - 1], _bandVolumes[i], _bands.Count - 1);
        }

        // reading current samples
        float[] samples = new float[SAMPLE_COUNT];
        _audioSource.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);


        float[] bandVolumes = new float[_bands.Count - 1];
        for (int i = 1; i < _bands.Count; i++)
        {
            float db = BandVol(_bands[i - 1], _bands[i], samples) * _dbMulti;
            bandVolumes[i - 1] = db;
            // Debug.Log(i.ToString() + " " + db);
        }

        _bandVolumes[0] = bandVolumes;
    }

    public static float BandVol(float fLow, float fHigh, float[] samples)
    {
        float hzStep = 20000 / SAMPLE_COUNT;

        int samples_count = Mathf.RoundToInt((fHigh - fLow) / hzStep);

        int firtSample = Mathf.RoundToInt(fLow / hzStep);
        int lastSample = Mathf.Min(firtSample + samples_count, SAMPLE_COUNT - 1);

        
        float sum = 0;
        // average the volumes of frequencies fLow to fHigh
        for (int i = firtSample; i <= lastSample; i++)
        {
            sum += samples[i];
        }
        return sum;
    }
}
