using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateNOR : Joinable
{
    void Update()
    {
        List<bool> inputs = base.GetInputs();
        bool success = true;

        //added the if statement that checks the size of the inputs list. without that it returns true even if there are no inputs
        if (inputs.Count > 0)
        {
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
}
