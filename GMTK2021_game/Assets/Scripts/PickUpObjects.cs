using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    [SerializeField] private bool isPickedUp;
    [SerializeField] private float rayDistance;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private float throwVelocity;

    private Vector2 rayDirection = Vector2.right;
    private Vector2 axis;

    private bool holdButtonDown;
    private bool holdButtonUp;

    void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }

    void Update()
    {
        axis.x = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.E))
        {
            holdButtonDown = true;
            holdButtonUp = false;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            holdButtonDown = false;
            holdButtonUp = true;
        }
        /*else
        {
            holdButtonDown = false;
            holdButtonUp = false;
        }*/
    }

    void FixedUpdate()
    {
        FlipRay();

        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, rayDirection * transform.localScale, rayDistance);

        if (holdButtonDown)
         {
            if (raycastHit2D.collider != null && raycastHit2D.collider.CompareTag("CanPickUp"))
            {
                raycastHit2D.collider.gameObject.transform.parent = holdPoint;
                raycastHit2D.collider.gameObject.transform.position = holdPoint.position;
                raycastHit2D.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            }
            holdButtonDown = false;
         }
         else if (holdButtonUp)
         {
            if (raycastHit2D.collider != null && raycastHit2D.collider.CompareTag("CanPickUp"))
            {
                raycastHit2D.collider.gameObject.transform.parent = null;
                raycastHit2D.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                raycastHit2D.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1) * throwVelocity;
            }
            holdButtonUp = false;
         }

    }

    void FlipRay()
    {
        if (axis.x < 0)
        {
            rayDirection = Vector2.left;
        }
        else if (axis.x > 0)
        {
            rayDirection = Vector2.right;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + rayDistance * (Vector3)rayDirection);
    }
}
