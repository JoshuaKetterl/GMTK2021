using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Input Only Join
 * ---------------
 * Joinable that cannot output to others, usable for door controls etc.
 */
public class InputOnlyJoin : Joinable
{
    // Clear Implementations of mouse drag so output connections cannot be made
    public override void OnMouseDown(){}

    public override void OnMouseDrag(){}

    public override void OnMouseUp(){}
}
