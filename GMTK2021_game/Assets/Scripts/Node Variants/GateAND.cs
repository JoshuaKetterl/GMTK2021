using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAND : Joinable
{
    void Update()
    {
        List<bool> inputs = base.GetInputs();
        int trueCount = 0;
        bool fail = false;

        foreach(bool b in inputs)
        {
            if (b)
            {
                trueCount++;
            }
            else
            {
                fail = true;
            }
        }
        if(trueCount > 2 && !fail)
        {
            base.output = true;
        }
        else
        {
            base.output = false;
        }
    }
}
