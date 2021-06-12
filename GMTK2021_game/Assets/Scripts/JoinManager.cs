using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* --------------
 *  Join Manager
 * --------------
 * Stores list of Joinables and renders joins, as well as issuing unique IDs for Joins and Joinables
 * 
 */

public class JoinManager : MonoBehaviour
{
    public List<Joinable> nodes;
    static int idCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        nodes = new List<Joinable>();
    }

    // Update is called once per frame
    void Update()
    {
        RenderJoins();
    }

    private void RenderJoins()
        //TODO potentially
    {
        foreach(Joinable output in nodes)
        {
            foreach (Joinable input in output.joinedOutputs)
            {
                Vector3 midpoint = new Vector3(input.transform.position.x, output.transform.position.y);
                RenderJoin(input.transform.position, midpoint);
                RenderJoin(midpoint, output.transform.position);
            }
        }
    }

    private void RenderJoin(Vector3 start, Vector3 end)
    {
        //render connection here
    }

    public void AddNode(Joinable j)
    {
        nodes.Add(j);
    }

    public void RemoveNode(Joinable j)
    {
        //TODO Make sure this works!
        nodes.RemoveAll(x => x.ID == j.ID);
    }

    public static int GetUniqueID()
    {
        return idCounter++;
    }
}
