using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Spectogram3D : MonoBehaviour
{
    public string _musicObjectTag = "MusicObject"; // remember to add this in unity inspector

    Vector3[] verts;
    int[] triangles;



    // Start is called before the first frame update
    void Start()
    {
        // https://docs.unity3d.com/ScriptReference/MeshFilter-mesh.html
        //Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        //Mesh mesh2 = Instantiate(mesh);
        //GetComponent<MeshFilter>().sharedMesh = mesh2;



        // InvokeRepeating("CloneMesh", 0.0f, 2f);

        CreateMesh();
    }

    private void CreateMesh()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ReCalculateMesh();


        //GameObject[] cubes;
        //cubes = GameObject.FindGameObjectsWithTag(_musicObjectTag);

        //foreach(GameObject cube in cubes)
        //{
        //    cube.transform.position = new Vector3(cube.transform.position.x, cube.transform.position.y, cube.transform.position.z + 0.01f);
        //}
        
    }

    void CloneMesh()
    {
        GameObject gameObject = Instantiate(GetComponent<MeshRenderer>().gameObject);
        gameObject.tag = _musicObjectTag;

        Destroy(gameObject, 3);
    }


    void ReCalculateMesh()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        List<Vector3> verts = new List<Vector3>();
        List<int> triangles = new List<int>();

        float xRange = 100;

        for(int m=0; m<AudioManager._bandVolumes.Length - 1; m++)
        {
            float[] currentVolumes = AudioManager._bandVolumes[m];
            float[] previousVolumes = AudioManager._bandVolumes[m + 1];

            float zBandValue = m * 4;
            float zBandNextValue = (m + 1) * 4;

            for (int i = 0; i < currentVolumes.Length - 1; i++)
            {
                // calculating x position 
                float x = ((float)i / (currentVolumes.Length - 2)) * xRange;
                float xNext = ((float)(i + 1) / (currentVolumes.Length - 2)) * xRange;
                float volume = currentVolumes[i];
                float voulumeNext = currentVolumes[i + 1];

                // two volumes that was previous
                float volumePrevious = previousVolumes[i];
                float volumeNextPrevious = previousVolumes[i + 1];

                if(m==0)
                    GenerateFrontFace(x, xNext, volume, voulumeNext, verts, triangles, zBandValue);

                // connection with previous band

                // adding verst connecting this band with the next one
                verts.Add(new Vector3(x, volume, zBandValue));
                verts.Add(new Vector3(xNext, voulumeNext, zBandValue));
                verts.Add(new Vector3(x, volumePrevious, zBandNextValue));
                verts.Add(new Vector3(xNext, volumeNextPrevious, zBandNextValue));

                int start_point = verts.Count - 4;
                // adding 2 triangles using this vertex
                triangles.Add(start_point + 0);
                triangles.Add(start_point + 2);
                triangles.Add(start_point + 1);

                triangles.Add(start_point + 2);
                triangles.Add(start_point + 3);
                triangles.Add(start_point + 1);

                // left side
                if(i == 0)
                {
                    verts.Add(new Vector3(x, 0, zBandValue));
                    verts.Add(new Vector3(x, 0, zBandNextValue));
                    verts.Add(new Vector3(x, volume, zBandValue));
                    verts.Add(new Vector3(x, volumePrevious, zBandNextValue));

                    start_point = verts.Count - 4;
                    // adding 2 triangles using this vertex
                    triangles.Add(start_point + 0);
                    triangles.Add(start_point + 1);
                    triangles.Add(start_point + 2);

                    triangles.Add(start_point + 1);
                    triangles.Add(start_point + 3);
                    triangles.Add(start_point + 2);
                }

                // right side
                if(i == currentVolumes.Length - 2)
                {
                    verts.Add(new Vector3(xNext, 0, zBandValue));
                    verts.Add(new Vector3(xNext, 0, zBandNextValue));
                    verts.Add(new Vector3(xNext, volume, zBandValue));
                    verts.Add(new Vector3(xNext, volumePrevious, zBandNextValue));

                    start_point = verts.Count - 4;
                    // adding 2 triangles using this vertex
                    triangles.Add(start_point + 0);
                    triangles.Add(start_point + 2);
                    triangles.Add(start_point + 1);

                    triangles.Add(start_point + 1);
                    triangles.Add(start_point + 2);
                    triangles.Add(start_point + 3);
                }

            }

        }
        


        //mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0) };
        //mesh.triangles = new int[] { 0, 1, 2 };

        mesh.vertices = verts.ToArray();
        mesh.triangles = triangles.ToArray();

        mesh.RecalculateNormals();
    }

    private void GenerateFrontFace(float x, float x_next, float volume, float volume_next, List<Vector3> verts, List<int> triangles, float zBandValue)
    {
        // this algoritm can be better, I don't need adding vertex of "next band"

        // adding verst connecting this band with the next one
        verts.Add(new Vector3(x, 0, zBandValue));
        verts.Add(new Vector3(x, volume, zBandValue));
        verts.Add(new Vector3(x_next, 0, zBandValue));
        verts.Add(new Vector3(x_next, volume_next, zBandValue));

        int start_point = verts.Count - 4;
        // adding 2 triangles using this vertex
        triangles.Add(start_point + 0);
        triangles.Add(start_point + 1);
        triangles.Add(start_point + 2);

        triangles.Add(start_point + 1);
        triangles.Add(start_point + 3);
        triangles.Add(start_point + 2);


    }
}
