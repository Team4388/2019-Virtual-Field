using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    public bool nyah = false;
    public bool negNyah = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left"))
        {
            transform.Rotate(Vector3.up, -200 * Time.deltaTime);
            nyah = true;
            negNyah = false;
        }
        if (Input.GetKey("right"))
        {
            transform.Rotate(Vector3.up, 200 * Time.deltaTime);
            negNyah = true;
            nyah = false;
        }
    }
}
