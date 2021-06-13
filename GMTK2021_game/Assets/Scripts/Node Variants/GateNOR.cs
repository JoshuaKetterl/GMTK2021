using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateNOR : Joinable
{
    void Update()
    {
        List<bool> inputs = base.GetInputs();
        bool success = true;

        foreach (bool b in inputs)
        {
            if (b)
            {
                success = false;
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
