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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual List<bool> getInput()
    {
        List<bool> results = new List<bool>();
        foreach(Joinable input in joinedInputs)
        {
            results.Add(input.getOutput);
        }

        return results;
    }

    public bool getOutput()
    {
        return output;
    }
}
