using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinPool : MonoBehaviour
{

    [SerializeField] private GameObject pooledJoin;

    private readonly int maxJoins = 256;
    private bool notEnoughJoinsInPool = true;

    private int joinCount = 0;
    private List<GameObject> joins;

    // Start is called before the first frame update
    void Start()
    {
        joins = new List<GameObject>();
    }

    public GameObject GetJoin()
    {
        if (joins.Count > 0)
        {
            for (int i = 0; i < joins.Count; i++)
            {
                if (!joins[i].activeInHierarchy)
                {
                    return joins[i];
                }
            }
        }

        if (notEnoughJoinsInPool)
        {
            GameObject b = Instantiate(pooledJoin);
            b.SetActive(false);
            joins.Add(b);
            if (++joinCount > maxJoins)
                notEnoughJoinsInPool = false;
            return b;
        }
        return null;
    }
}
