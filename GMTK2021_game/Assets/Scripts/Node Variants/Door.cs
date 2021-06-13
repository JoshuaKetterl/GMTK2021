using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Joinable doorSwitchNode;
    [SerializeField] private Animator animator;

    [FMODUnity.EventRef] public string DoorOpenEvent = "";
    [FMODUnity.EventRef] public string DoorCloseEvent = "";

    // Update is called once per frame
    void Update()
    {
        if (doorSwitchNode.GetOutput())
        {
            animator.SetTrigger("Open");
            FMODUnity.RuntimeManager.PlayOneShot(DoorOpenEvent, transform.position);
        }
        else
        {
            animator.SetTrigger("Close");
            FMODUnity.RuntimeManager.PlayOneShot(DoorCloseEvent, transform.position);
        }
    }
}
