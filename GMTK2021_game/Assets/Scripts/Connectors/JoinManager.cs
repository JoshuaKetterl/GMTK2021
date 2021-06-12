using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* --------------
 *  Join Manager
 * --------------
 * Stores list of Joinables and renders joins, as well as issuing unique IDs for Joins and Joinables
 * All joinables in scene must be added to the serialized "nodes" list to work properly
 */

public class JoinManager : MonoBehaviour
{
    static int idCounter = 0;

    [SerializeField] List<Joinable> nodes;
    [SerializeField] JoinPool HorizontalJoins;
    [SerializeField] JoinPool VerticalJoins;
    [SerializeField] JoinPool ElbowJoins;

    static Join mouseJoinHorizontal;
    static Join mouseJoinVertical;
    static Join mouseJoinElbow;

    public float joinSpriteScale = 10f;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Joinable node in nodes)
        {
            node.joinManager = this;
        }
    }

    // Update is called once per frame
    public void UpdateStaticJoins()
    {
        RenderJoins();
    }

    public static void UpdateMouseJoin()
    {

    }

    public static void RemoveMouseJoin()
    {

    }

    public void RenderJoins()
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

    public void RenderJoin(Vector3 start, Vector3 end)
    {
        Vector3 elbow = new Vector3(start.x, end.y);
        Vector3 horzMidpoint, vertMidpoint;

        float hScale = Math.Abs(start.x - end.x) * joinSpriteScale;
        float vScale = Math.Abs(start.y - end.y) * joinSpriteScale;

        //DEBUG
        print("diff: " + Math.Abs(start.x - end.x) + "   hScale: " + hScale);

        if (end.x > start.x)
        {
            if(end.y > start.y)
            { // Q1 Upper Right
                vertMidpoint = new Vector3(start.x, start.y + (Math.Abs(start.y - elbow.y)) / 2);
                horzMidpoint = new Vector3(end.x - (Math.Abs(end.x - elbow.x) / 2), end.y);
            }
            else
            { // Q4 Lower Right
                vertMidpoint = new Vector3(start.x, start.y - (Math.Abs(start.y - elbow.y)) / 2);
                horzMidpoint = new Vector3(end.x - (Math.Abs(end.x - elbow.x) / 2), end.y);
            }
        }
        else
        {
            if (end.y > start.y)
            { // Q2 Upper Left
                vertMidpoint = new Vector3(start.x, start.y + (Math.Abs(start.y - elbow.y)) / 2);
                horzMidpoint = new Vector3(end.x + (Math.Abs(end.x - elbow.x) / 2), end.y);
            }
            else
            { // Q3 Lower Left
                vertMidpoint = new Vector3(start.x, start.y - (Math.Abs(start.y - elbow.y)) / 2);
                horzMidpoint = new Vector3(end.x + (Math.Abs(end.x - elbow.x) / 2), end.y);
            }
        }
        DrawJoin(horzMidpoint, vertMidpoint, elbow, hScale, vScale);
    }

    public void DrawJoin(Vector3 horzMidpoint, Vector3 vertMidpoint, Vector3 elbow, float hScale, float vScale)
    {
        GameObject joinH = HorizontalJoins.GetJoin();
        GameObject joinV = VerticalJoins.GetJoin();
        GameObject joinE = ElbowJoins.GetJoin();

        joinH.transform.position = horzMidpoint;
        joinV.transform.position = vertMidpoint;
        joinE.transform.position = elbow;

        joinH.transform.localScale = new Vector3(hScale, 1);
        joinV.transform.localScale = new Vector3(1, vScale);

        joinH.SetActive(true);
        joinV.SetActive(true);
        joinE.SetActive(true);
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
