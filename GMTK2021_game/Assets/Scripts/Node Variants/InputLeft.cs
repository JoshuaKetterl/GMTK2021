using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLeft : OutputOnlyJoin
{
    void Update()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            base.output = true;
        }
        else
        {
            base.output = false;
        }
    }
}
