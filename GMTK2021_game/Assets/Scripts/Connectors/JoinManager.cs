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

    GameObject mouseJoinHorizontal;
    GameObject mouseJoinVertical;
    GameObject mouseJoinElbow;

    public float joinSpriteScale = 10f;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        foreach (Joinable node in nodes)
        {
            node.target = this.target;
            node.joinManager = this;
        }

        mouseJoinHorizontal = Instantiate(HorizontalJoins.pooledJoin);
        mouseJoinVertical = Instantiate(VerticalJoins.pooledJoin);
        mouseJoinElbow = Instantiate(ElbowJoins.pooledJoin);

        mouseJoinHorizontal.SetActive(false);
        mouseJoinVertical.SetActive(false);
        mouseJoinElbow.SetActive(false);
    }

    // Update is called once per frame
    public void UpdateStaticJoins()
    {
        HorizontalJoins.DisableAll();
        VerticalJoins.DisableAll();
        ElbowJoins.DisableAll();

        foreach (Joinable output in nodes)
        {
            foreach (Joinable input in output.joinedOutputs)
            {
                RenderJoin(output.transform.position, input.transform.position);
            }
        }
    }

    public void UpdateMouseJoin(Vector3 start, Vector3 end)
    {
        Vector3 elbow = new Vector3(start.x, end.y, -0.2f);
        Vector3 horzMidpoint, vertMidpoint;
        Quaternion eRotation = Quaternion.identity;

        float hScale = Math.Abs(start.x - end.x) * joinSpriteScale;
        float vScale = Math.Abs(start.y - end.y) * joinSpriteScale;

        //DEBUG
        print("diff: " + Math.Abs(start.x - end.x) + "   hScale: " + hScale);

        if (end.x > start.x)
        {
            if (end.y > start.y)
            { // Q1 Upper Right
                vertMidpoint = new Vector3(start.x, start.y + (Math.Abs(start.y - elbow.y)) / 2);
                horzMidpoint = new Vector3(end.x - (Math.Abs(end.x - elbow.x) / 2), end.y);
                eRotation.x = 90;
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
                eRotation.z = 90;
            }
            else
            { // Q3 Lower Left
                vertMidpoint = new Vector3(start.x, start.y - (Math.Abs(start.y - elbow.y)) / 2);
                horzMidpoint = new Vector3(end.x + (Math.Abs(end.x - elbow.x) / 2), end.y);
                eRotation.y = 90;
            }
        }

        mouseJoinHorizontal.transform.position = horzMidpoint;
        mouseJoinVertical.transform.position = vertMidpoint;
        mouseJoinElbow.transform.position = elbow;

        mouseJoinHorizontal.transform.localScale = new Vector3(hScale, 1);
        mouseJoinVertical.transform.localScale = new Vector3(1, vScale);
        mouseJoinElbow.transform.localRotation = eRotation;

        mouseJoinHorizontal.SetActive(true);
        mouseJoinVertical.SetActive(true);
        mouseJoinElbow.SetActive(true);
    }

    public void RemoveMouseJoin()
    {
        mouseJoinHorizontal.gameObject.SetActive(false);
        mouseJoinVertical.gameObject.SetActive(false);
        mouseJoinElbow.gameObject.SetActive(false);
    }

    public void RenderJoin(Vector3 start, Vector3 end)
    {
        Vector3 elbow = new Vector3(start.x, end.y, -0.2f);
        Vector3 horzMidpoint, vertMidpoint;

        float hScale = Math.Abs(start.x - end.x) * joinSpriteScale;
        float vScale = Math.Abs(start.y - end.y) * joinSpriteScale;
        Quaternion eRotation = Quaternion.identity;

        //DEBUG
        print("diff: " + Math.Abs(start.x - end.x) + "   hScale: " + hScale);

        if (end.x > start.x)
        {
            if(end.y > start.y)
            { // Q1 Upper Right
                vertMidpoint = new Vector3(start.x, start.y + (Math.Abs(start.y - elbow.y)) / 2);
                horzMidpoint = new Vector3(end.x - (Math.Abs(end.x - elbow.x) / 2), end.y);
                eRotation.x = 90;
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
                eRotation.z = 90;
            }
            else
            { // Q3 Lower Left
                vertMidpoint = new Vector3(start.x, start.y - (Math.Abs(start.y - elbow.y)) / 2);
                horzMidpoint = new Vector3(end.x + (Math.Abs(end.x - elbow.x) / 2), end.y);
                eRotation.y = 90;
            }
        }
        DrawJoin(horzMidpoint, vertMidpoint, elbow, hScale, vScale, eRotation);
    }

    public void DrawJoin(Vector3 horzMidpoint, Vector3 vertMidpoint, Vector3 elbow, float hScale, float vScale, Quaternion eRotation)
    {
        GameObject joinH = HorizontalJoins.GetJoin();
        GameObject joinV = VerticalJoins.GetJoin();
        GameObject joinE = ElbowJoins.GetJoin();

        joinH.transform.position = horzMidpoint;
        joinV.transform.position = vertMidpoint;
        joinE.transform.position = elbow;

        joinH.transform.localScale = new Vector3(hScale, 1);
        joinV.transform.localScale = new Vector3(1, vScale);
        joinE.transform.localRotation = eRotation;

        //joinH.GetComponent<Join>().timeToLive = ttl;
        //joinV.GetComponent<Join>().timeToLive = ttl;
        //joinE.GetComponent<Join>().timeToLive = ttl;

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
        idCounter++;
        return idCounter;
    }
}
