using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDown : BoxLogic
{
    public override bool ActivateBasedOnMovementDirection()
    {
        if (base.Movement.y < 0)
            return true;
        else
            return false;
    }
}
