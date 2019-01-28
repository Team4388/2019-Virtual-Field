using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setPosition : MonoBehaviour
{
    robotControl robotControl;

    // Start is called before the first frame update
    public void Start()
    {
        GameObject Robot = GameObject.Find("Robot");
        robotControl = Robot.GetComponent<robotControl>();
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
                robotControl.pos = new Vector3(-50, 10, -260);
            }
            else if (dd.value == 2)
            {
                robotControl.pos = new Vector3(0, 10, -260);
            }
            else if (dd.value == 3)
            {
                robotControl.pos = new Vector3(50, 10, -260);
            }
            robotControl.initialRot = robotControl.yRot;
            dd.value = 0;
        }
    }
}
