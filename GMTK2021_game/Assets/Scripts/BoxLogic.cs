using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoxLogic : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Vector3 movement;
    protected Vector3 Movement { get => movement; }

    protected virtual void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        movement = playerMovement.Movement;
    }

    protected virtual void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    public abstract bool ActivateBasedOnMovementDirection();
}
