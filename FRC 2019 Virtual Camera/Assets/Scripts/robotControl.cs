using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class robotControl : MonoBehaviour
{
    public float yRot, yAng;
    public float avgEncoder, lastEncoder;
    public Vector3 pos;
    public Vector3 rot;
    robotCommunication roboCom;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Robot = GameObject.Find("Robot");
        roboCom = Robot.GetComponent<robotCommunication>();
        pos = new Vector3(0, 10, -260);
        rot = new Vector3(0.0f, 0.0f, 0.0f);

        gameObject.transform.position = pos;
        roboCom.robotValues.TryGetValue("encoder", out avgEncoder);
        lastEncoder = avgEncoder;
    }

    // Update is called once per frame
    void Update()
    {
        roboCom.robotValues.TryGetValue("encoder", out yRot);
        roboCom.robotValues.TryGetValue("encoder", out avgEncoder);
        yAng = (yRot + 90) * Mathf.Deg2Rad;
        pos.x += ((avgEncoder - lastEncoder) * Mathf.Cos(yAng));
        pos.z += ((avgEncoder - lastEncoder) * Mathf.Sin(yAng));
        ///TODO: Adjust yRot to fall within -180 to 180
        rot.y = yRot;
        transform.rotation = Quaternion.Euler(rot);
        transform.position = pos;
        lastEncoder = avgEncoder;
    }
}
