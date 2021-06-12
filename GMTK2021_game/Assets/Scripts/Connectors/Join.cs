using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Join : MonoBehaviour
{
    public float timeToLive = 0.1f;

    private void OnEnable()
    {
        Invoke(nameof(Remove), timeToLive);
    }

    private void Remove()
    {
        gameObject.SetActive(false);
    }
}
