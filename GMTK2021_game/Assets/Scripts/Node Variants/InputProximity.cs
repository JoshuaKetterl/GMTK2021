using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProximity : OutputOnlyJoin
{

    private GameObject player;
    [SerializeField] float range = 5;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < range)
        {
            base.output = true;
        }
        else
        {
            base.output = false;
        }
    }
}
