using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setPosition : MonoBehaviour
{
    robotNavXControl robotControl;

    // Start is called before the first frame update
    public void Start()
    {
        GameObject Robot = GameObject.Find("Robot");
        robotControl = Robot.GetComponent<robotNavXControl>();
    }

    // Update is called once per frame
    public void Update()
    {
        Dropdown dd = GetComponent<Dropdown>();
        // 1 = left, 2 = middle, and 3 = right
        if (dd.value != 0)
        {
            if (dd.value == 1)
            {
                robotControl.transform.position = new Vector3(-50, 0, -256);
            }
            else if (dd.value == 2)
            {
                robotControl.transform.position = new Vector3(0, 0, -256);
            }
            else if (dd.value == 3)
            {
                robotControl.transform.position = new Vector3(50, 0, -256);
            }
            robotControl.initRot = robotControl.rot;
            dd.value = 0;
        }
    }
}
