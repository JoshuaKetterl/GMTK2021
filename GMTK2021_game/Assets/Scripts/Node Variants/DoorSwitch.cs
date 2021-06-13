using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : InputOnlyJoin
{
    void Update()
    {
        List<bool> inputs = base.GetInputs();
        bool success = false;

        foreach (bool b in inputs)
        {
            if (b)
            {
                success = true;
            }
        }
        if (success)
        {
            base.output = true;
        }
        else
        {
            base.output = false;
        }
    }
}
