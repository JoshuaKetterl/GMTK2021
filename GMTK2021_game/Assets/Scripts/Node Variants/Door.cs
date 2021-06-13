using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Joinable doorSwitchNode;
    [SerializeField] private Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (doorSwitchNode.GetOutput())
        {
            animator.SetTrigger("Open");
        }
        else
        {
            animator.SetTrigger("Close");
        }
    }
}
