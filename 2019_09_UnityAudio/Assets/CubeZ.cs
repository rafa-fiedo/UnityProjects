using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class CubeZ : MonoBehaviour
{
    public GameObject _gameObject;
    public int _band;
    public float _heightMultipler = 5;

    private GameObject _nonStaticObj;

    // Start is called before the first frame update
    void Start()
    {
        Renderer thisRenderer = GetComponent<Renderer>();

        GameObject new_obj = Instantiate(_gameObject);
        new_obj.transform.position = gameObject.transform.position;

        new_obj.GetComponent<Renderer>().material = thisRenderer.material;
        new_obj.GetComponent<Renderer>().lightmapIndex = thisRenderer.lightmapIndex;
        new_obj.GetComponent<Renderer>().lightmapScaleOffset = thisRenderer.lightmapScaleOffset;

        _nonStaticObj = new_obj;

        thisRenderer.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        float height = (AudioManager._freqBand[_band] * _heightMultipler) + 1;
        _nonStaticObj.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, height);

        BoxCollider box = GetComponent<BoxCollider>();
        height /= 2.0f;

        box.size = new Vector3(box.size.x, box.size.y, height *1.15f);

        
    }
}
