using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*  Joinable
 * ----------
 * Base parent for Objects that can be joined together
 */
public class Joinable : MonoBehaviour
{
    public int ID;
    public bool output = false;
    public bool joinInProgress = false;

    public JoinManager joinManager;

    public List<Joinable> joinedInputs; // Nodes which connect to this Nodes Input
    public List<Joinable> joinedOutputs; // Nodes which this Node outputs a signal to

    // Start is called before the first frame update
    void Start()
    {
        joinedInputs = new List<Joinable>();
        joinedOutputs = new List<Joinable>();

        ID = JoinManager.GetUniqueID();
    }

    // Update is called once per frame
    void Update()
    {
        if (output)
        {
            // update appearance to active
        }
        else
        {
            //update appearance to inactive
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            RemoveAllConnections();
            joinManager.UpdateStaticJoins();
        }
    }

    public virtual void OnMouseDown()
    {
        joinInProgress = true;
    }

    public virtual void OnMouseDrag()
    {
        if (joinInProgress)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            joinManager.UpdateMouseJoin(transform.position, worldPosition);
        }
    }

    public virtual void OnMouseUp()
    {
        if (joinInProgress)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] hitColliders = Physics2D.OverlapPointAll(worldPosition);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                Joinable hit = hitColliders[i].gameObject.GetComponent<Joinable>();
                if(hit != null && hit.ID != this.ID)
                {
                    AddOutput(hit);
                }
            }
        }
        joinManager.RemoveMouseJoin();
        joinInProgress = false;
    }

    public virtual void AddOutput(Joinable output)
    {
        if (joinedOutputs.Any(x => x.ID == output.ID))
        { // if this connection already exists, remove it
            RemoveOutput(output);
            joinManager.UpdateStaticJoins();
        }
        else
        { // otherwise add the connection

            if (!(output is OutputOnlyJoin))
            { //If output is valid, form connection
                joinedOutputs.Add(output);
                output.joinedInputs.Add(this);
                joinManager.UpdateStaticJoins();
            } 
            // if output is Output Only Join (cannot take input) do nothing
        }
    }

    public virtual void RemoveOutput(Joinable output)
    {
        joinedOutputs.RemoveAll(x => x.ID == output.ID);
        output.joinedInputs.RemoveAll(x => x.ID == this.ID);
    }

    public virtual void RemoveAllConnections()
    {
        foreach (Joinable input in joinedInputs)
        {
            input.joinedOutputs.RemoveAll(x => x.ID == this.ID);
        }

        foreach (Joinable output in joinedOutputs)
        {
            output.joinedInputs.RemoveAll(x => x.ID == this.ID);
        }

        joinedInputs.Clear();
        joinedOutputs.Clear();
    }

    public List<bool> GetInputs()
    {
        List<bool> results = new List<bool>();
        foreach(Joinable input in joinedInputs)
        {
            results.Add(input.GetOutput());
        }
        return results;
    }

    /*
    public bool GetInput()
    {
        if (joinedInputs != null)
            return joinedInputs[0].gameObject.GetComponent<BoxLogic>().ActivateBasedOnMovementDirection();
        else
            return false;
    }*/

    public bool GetOutput()
    {
        return output;
    }

}
