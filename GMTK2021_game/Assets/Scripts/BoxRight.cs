using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRight : BoxLogic
{
    public override bool ActivateBasedOnMovementDirection()
    {
        if (base.Movement.x > 0)
            return true;
        else
            return false;
    }
}
