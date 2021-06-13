using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Joinable doorSwitchNode;

    // Update is called once per frame
    void Update()
    {
        if (doorSwitchNode.GetOutput())
        {
            //open door
        }
        else
        {
            //close door
        }
    }
}
