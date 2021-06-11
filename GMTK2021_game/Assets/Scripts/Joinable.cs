using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joinable : MonoBehaviour
{
    private bool output = false;

    private List<Joinable> joinedInputs;
    private List<Joinable> joinedOutputs;

    // Start is called before the first frame update
    void Start()
    {
        joinedInputs = new List<Joinable>();
        joinedOutputs = new List<Joinable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeConnection(Joinable input)
    {
        joinedInputs.Add(input);
    }

    public void RemoveConnection(Joinable output)
    {
        //TODO, lookup how to use List.Find by object reference
    }

    public void RemoveAllConnections()
    {
        foreach (Joinable input in joinedInputs)
        {
            //Cleanup joins here
        }

        foreach (Joinable output in joinedOutputs)
        {
            //Cleanup joins here
        }

        joinedInputs.Clear();
        joinedOutputs.Clear();
    }

    public virtual List<bool> GetInputs()
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
