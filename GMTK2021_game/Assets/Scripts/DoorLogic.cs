using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    private Joinable doorJoinable;
    private bool isOpened;

    void Start()
    {
        doorJoinable = GetComponent<Joinable>();
    }

    void Update()
    {
        if (doorJoinable.joinedInputs != null)
        {
            //isOpened = doorJoinable.GetInput();
        }

        if (isOpened)
        {
            //open door animation
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            //close door animation
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
