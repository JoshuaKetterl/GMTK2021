using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Join : MonoBehaviour
{
    private float timeToLive = -1f;

    private void OnEnable()
    {
        if(timeToLive > 0)
            Invoke(nameof(Remove), timeToLive);
    }

    private void Remove()
    {
        gameObject.SetActive(false);
    }
}
