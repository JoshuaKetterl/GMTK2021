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
                RenderJoin(input.transform.position, output.transform.position);
            }
        }
    }

    public static void RenderJoin(Vector3 start, Vector3 end)
    {
        Vector3 midpoint = new Vector3(start.x, end.y);
        DrawJoinSegment(start, midpoint);
        DrawJoinSegment(midpoint, end);
    }

    private static void DrawJoinSegment(Vector3 start, Vector3 end)
    {
        //Render One Line of connection here
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
