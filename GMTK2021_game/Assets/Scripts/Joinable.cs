using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Joinable : MonoBehaviour
{
    public int ID;
    private bool output = false;
    private bool joinInProgress = false;

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
        
    }

    private void OnMouseDown()
    {
        joinInProgress = true;
    }
    private void OnMouseDrag()
    {
        if (joinInProgress)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            JoinManager.RenderJoin(transform.position, worldPosition);
        }
    }

    private void OnMouseUp()
    {
        if (joinInProgress)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] hitColliders = Physics2D.OverlapPointAll(worldPosition);

            //DEBUG
            print("mouse pos:" + worldPosition.x + ", " + worldPosition.y);
            print("joinable pos:" + transform.position.x + ", " + transform.position.y);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].tag == "Joinable")
                {
                    AddOutput(hitColliders[i].gameObject.GetComponent<Joinable>());

                    //DEBUG
                    print("Connection Made");

                    break;
                }
            }
        }
        joinInProgress = false;
    }

    public void AddOutput(Joinable output)
    {
        if (joinedOutputs.Any(x => x.ID == output.ID))
        { // if this connection already exists, remove it
            RemoveOutput(output);
        }
        else
        { // otherwise add the connection
            joinedOutputs.Add(output);
            output.joinedInputs.Add(this);
        }
    }

    public void RemoveOutput(Joinable output)
    {
        joinedOutputs.RemoveAll(x => x.ID == output.ID);
        output.joinedInputs.RemoveAll(x => x.ID == this.ID);
    }

    public void RemoveAllConnections()
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

    public bool GetOutput()
    {
        return output;
    }

}
