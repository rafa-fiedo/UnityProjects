using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSampleObjects : MonoBehaviour
{
    public GameObject _sampleCubePrefab;
    public float _maxScale;
    public int _sampleRate = 512;
    public int _radius = 50;

    GameObject[] _sampleCubes;

    // Start is called before the first frame update
    void Start()
    {
        _sampleCubes = new GameObject[_sampleRate];

        float degree_step = 360.0f / _sampleRate;

        for (int i = 0; i < _sampleRate; i++)
        {
            GameObject instanceObj = Instantiate(_sampleCubePrefab);
            instanceObj.name = "Sample Object" + i;
            instanceObj.transform.parent = this.transform;
            this.transform.eulerAngles = new Vector3(0, -degree_step * i, 0);
            instanceObj.transform.position = Vector3.forward * _radius;

            _sampleCubes[i] = instanceObj;

        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _sampleRate; i++)
        {
            if (_sampleCubes == null)
                continue;
            _sampleCubes[i].transform.localScale = new Vector3(1, 1, AudioManager._samples[i] * _maxScale);
        }
    }
}
