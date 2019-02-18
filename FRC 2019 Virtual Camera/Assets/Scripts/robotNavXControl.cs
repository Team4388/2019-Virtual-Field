using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotNavXControl : MonoBehaviour
{
    public float distance, lastDistance;
    public Vector3 vel, acc, initRot, rot, otherRot;
    public int button;
    robotCommunication roboCom;
    bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Robot = GameObject.Find("Robot");
        roboCom = Robot.GetComponent<robotCommunication>();
        vel = new Vector3(0, 0, 0);
        rot = new Vector3(0.0f, 0.0f, 0.0f);
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0, y = 0, z = 0;
        //roboCom.robotValues.TryGetValue("Pitch", out rot.x);
        roboCom.robotValues.TryGetValue("Yaw Angle Deg", out rot.y);
        //roboCom.robotValues.TryGetValue("Roll", out rot.z);
        roboCom.robotValues.TryGetValue("Distance", out distance);
        rot.x *= -1;
        rot.z *= -1;
        //Angle
        //rot -= initRot;
        Vector3 finalRot = new Vector3(0, 0, 0);
        finalRot.y = rot.y - initRot.y;
        transform.rotation = Quaternion.Euler(finalRot);
        //Position
        transform.position += transform.forward * (distance - lastDistance);
        lastDistance = distance;
    }
}
