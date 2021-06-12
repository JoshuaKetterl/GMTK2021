using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D platform;
    private Vector3 movement;
    [SerializeField] private LayerMask layerMaskExcludingPlayer;
    private LayerMask defaultLayerMask;
    void Start()
    {
        platform = GetComponent<PlatformEffector2D>();
        defaultLayerMask = platform.colliderMask;
    }

    void Update()
    {
        movement.y = Input.GetAxis("Vertical");

        if (movement.y < 0)
            platform.colliderMask = layerMaskExcludingPlayer;
        else
            platform.colliderMask = defaultLayerMask;
    }
}
