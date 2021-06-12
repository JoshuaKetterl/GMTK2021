using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    [SerializeField] private bool isPickedUp;
    [SerializeField] private float rayDistance;
    [SerializeField] private Transform holdPoint;
    //[SerializeField] private Transform rayPoint;
    [SerializeField] private float throwVelocity;
    [SerializeField] private LayerMask layerMask;

    void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }

    void Update()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, rayDistance);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isPickedUp)
            {
                if (raycastHit2D.collider != null && raycastHit2D.collider.CompareTag("CanPickUp"))
                {
                    isPickedUp = true;
                }
            }
            else if(!Physics2D.OverlapPoint(holdPoint.position, layerMask))
            {
                isPickedUp = false;

                if (raycastHit2D.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    raycastHit2D.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwVelocity;
                }
            }
        }

        if (isPickedUp)
            raycastHit2D.collider.gameObject.transform.position = holdPoint.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + rayDistance * transform.localScale.x * Vector3.right);
    }
}
