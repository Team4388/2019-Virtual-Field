using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class robotControl : MonoBehaviour
{
    public float initialRot, yRot, yAng;
    public float avgEncoder, lastEncoder, leftEncoder, rightEncoder;
    public Vector3 pos, rot;
    public int button;
    robotCommunication roboCom;
    bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Robot = GameObject.Find("Robot");
        roboCom = Robot.GetComponent<robotCommunication>();
        pos = new Vector3(0, 10, -257.5f);
        rot = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        roboCom.robotValues.TryGetValue("Yaw Angle Deg", out yRot);
        roboCom.robotValues.TryGetValue("Left Pos Inches", out leftEncoder);
        roboCom.robotValues.TryGetValue("Right Pos Inches", out rightEncoder);
        /*if (!start && roboCom.isData)
        {
            avgEncoder = (leftEncoder + rightEncoder) / 2;
            lastEncoder = avgEncoder;
            initialRot = yRot;
            transform.position = pos;
            start = true;
        } else if (roboCom.isData) {
            avgEncoder = (leftEncoder + rightEncoder) / 2;
            yAng = (yRot - initialRot) * Mathf.Deg2Rad;
            float _yAng = yAng;
            pos.x -= (avgEncoder - lastEncoder) * Mathf.Cos(yAng);
            _yAng = yAng;
            pos.z += (avgEncoder - lastEncoder) * Mathf.Sin(yAng);
            rot.y = yAng * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(rot);
            transform.position = pos;
            lastEncoder = avgEncoder;
        }*/
    }
}
