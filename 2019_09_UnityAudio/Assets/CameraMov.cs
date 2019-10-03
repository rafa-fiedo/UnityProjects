using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    private float moveSpeed = 0.01f;

    bool firstStep = true;

    bool moveRight = true;

    bool orbit = false;

    float orbitPos = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.z > -31)
        {
            moveSpeed = 0.02f;
            firstStep = false;
        }

        if(transform.position.x > 10)
        {
            moveRight = false;
        }

        if (transform.position.x < -8)
        {
            orbit = true;
        }

        if (orbit)
        {

            transform.right += new Vector3(0.2f, 0, 0);
            orbitPos++;
            transform.LookAt(new Vector3(0, 0, 25));
            return;
        }
    

        if (firstStep)
        {
            transform.position += moveSpeed * Vector3.forward;
        }
        else
        {
            if (moveRight)
            {
                transform.position += moveSpeed * Vector3.right;
            }
            else
            {
                transform.position += moveSpeed * Vector3.left;
            }
                
        }

        transform.LookAt(new Vector3(0, 0, 25));

        Debug.Log(transform.position);
    }
}
