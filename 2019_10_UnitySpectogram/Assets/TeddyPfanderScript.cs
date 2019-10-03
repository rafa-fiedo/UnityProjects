using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyPfanderScript : MonoBehaviour
{
    public GameObject go1;
    public GameObject go2;
    public GameObject go3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] objs = new GameObject[3];
        objs[0] = go1;
        objs[1] = go2;
        objs[2] = go3;


        List<Vector3> verts = new List<Vector3>();
        foreach(GameObject obj in objs)
        {
            Vector3 newPosition = obj.transform.position + (Vector3.forward * 0.02f);
            obj.transform.position = newPosition;
            verts.Add(newPosition);
        }

        // logic for triangle 

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        mesh.vertices = verts.ToArray();
        mesh.triangles = new int[] { 0, 1, 2 }; // REMEMBER THIS IS ONLY ONE SIDE, so try also 1,0,2 or rotate camera

    }
}
