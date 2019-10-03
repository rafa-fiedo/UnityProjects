using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class Cubes : MonoBehaviour
{
    public GameObject _object;
    public float _distance_between_objects = 10;
    public float _heightMultipler = 10;

    private GameObject[] _objects = new GameObject[8];

    // Start is called before the first frame update
    void Start()
    {
        Renderer thisRenderer = GetComponent<Renderer>();

        Vector3 startPositionOffset = Vector3.right * (_distance_between_objects * 3.5f);

        for(int x=0; x<8; x++)
        {
            GameObject new_obj = Instantiate(_object);
            new_obj.transform.position = this.transform.position + (Vector3.right * x * _distance_between_objects) - startPositionOffset;
            new_obj.GetComponent<Renderer>().material = thisRenderer.material;
            new_obj.GetComponent<Renderer>().lightmapIndex = thisRenderer.lightmapIndex;
            new_obj.GetComponent<Renderer>().lightmapScaleOffset = thisRenderer.lightmapScaleOffset;

            _objects[x] = new_obj;
        }
        thisRenderer.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int x=0; x<8; x++)
        {
            GameObject obj = _objects[x];

            float height = (AudioManager._freqBand[x] * _heightMultipler) + 1;
            obj.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, height);
        }
    }
}
