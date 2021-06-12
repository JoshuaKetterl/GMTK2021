using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Join : MonoBehaviour
{
    public int ID;
    public Joinable output;
    public Joinable input;

    public Vector3 midPoint;

    private void Start()
    {
        midPoint = new Vector3(output.transform.position.x, input.transform.position.y);
    }

    public Join(Joinable output, Joinable input)
    {
        this.output = output;
        this.input = input;
        this.ID = JoinManager.GetUniqueID();
    }
}
