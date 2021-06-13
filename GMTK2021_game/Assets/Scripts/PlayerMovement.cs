using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [FMODUnity.EventRef] public string JumpEvent = "";
    [FMODUnity.EventRef] public string LandingEvent = "";

    private Vector3 movement;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    private bool canJump;
    private bool jumpButtonPressed;
    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private float jumpVelocity = 9f;
    [SerializeField] private float fallGravity = 6f;
    [SerializeField] private float lowJumpGravity = 5f;
    [SerializeField] private LayerMask raycastLayerMask;

    [SerializeField] private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    [SerializeField] private float registerJumpBeforeLanding = 0.15f;
    private float registerJumpBeforeLandingTimer;
    private bool jumpAfterLanding;

    private SpriteRenderer playerSpriteRenderer;

    private bool isClimbing;

    private Animator animator;

    public Vector3 Movement { get => movement; }

    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressed = true;
            registerJumpBeforeLandingTimer = registerJumpBeforeLanding;
        }
        else
        {
            jumpButtonPressed = false;
            registerJumpBeforeLandingTimer -= Time.deltaTime;
        }

        if (registerJumpBeforeLandingTimer > 0)
            jumpAfterLanding = true;
        else
            jumpAfterLanding = false;

        if (CheckGround())
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        if ((CheckGround() && jumpButtonPressed) || (coyoteTimeCounter > 0 && jumpButtonPressed))
            canJump = true;

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        //transform.position += movementSpeed * Time.deltaTime * movement;
        rb.velocity = new Vector2(movement.x * movementSpeed, rb.velocity.y);

        if (movement.y != 0)
            isClimbing = true;
        else
            isClimbing = false;

        FlipPlayer();

        Animations();
        FallingAnimation();

        print(rb.gravityScale);
    }

    private void FixedUpdate()
    {
        //LadderClimb();

        /*if (!isClimbing && rb.gravityScale != 0)
        {*/
        if (canJump || (CheckGround() && jumpAfterLanding))
        {
            Jump();
        }

        if (canJump || CheckGround() || coyoteTimeCounter > 0)
        {
            rb.gravityScale = 1f;
        }
        else if (rb.velocity.y < jumpVelocity / 1.7)
        {
            rb.gravityScale = fallGravity;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpGravity;
        }
        else rb.gravityScale = 1f;
        //}
    }

    private bool CheckGround()
    {
        float offsetBeyondPlayerCollider = 0.04f;
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, offsetBeyondPlayerCollider, raycastLayerMask);

        Color rayColor;
        if (raycastHit2D.collider != null)
            rayColor = Color.green;
        else
            rayColor = Color.red;

        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + offsetBeyondPlayerCollider), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + offsetBeyondPlayerCollider), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, boxCollider2D.bounds.extents.y + offsetBeyondPlayerCollider), Vector2.right * boxCollider2D.bounds.extents.y, rayColor);
        return raycastHit2D.collider != null;
    }

    private void Jump()
    {
        animator.SetBool("Jumping", true);
        animator.SetBool("Falling", false);
        animator.SetBool("Walking", false);
        animator.SetBool("Climbing", false);
        animator.SetBool("Idle", false);

        rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
        FMODUnity.RuntimeManager.PlayOneShot(JumpEvent, transform.position);

        canJump = false;
        coyoteTimeCounter = 0;
        //registerJumpBeforeLandingTimer = 0;
        jumpAfterLanding = false;
    }

    private void FlipPlayer()
    {
        if (movement.x < 0)
            playerSpriteRenderer.flipX = true;
        else if (movement.x > 0)
            playerSpriteRenderer.flipX = false;
    }

    private void LadderClimb()
    {
        if (isClimbing)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
            animator.SetBool("Walking", false);
            animator.SetBool("Climbing", true);
            animator.SetBool("Idle", false);

            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            rb.velocity = new Vector2(rb.velocity.x, movement.y * movementSpeed);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            LadderClimb();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            rb.gravityScale = fallGravity;
        }
    }

    private void Animations()
    {
        if (!(animator.GetBool("Jumping")) && !(animator.GetBool("Falling")) && !(animator.GetBool("Walking")) && !(animator.GetBool("Climbing")))
        {
            animator.SetBool("Idle", true);
        }

        if (movement.x != 0 && rb.velocity.y <= 0)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
            animator.SetBool("Walking", true);
            //animator.SetBool("Climbing", false);
            animator.SetBool("Idle", false);
        }
        else if ((movement.x == 0 & movement.y == 0 && CheckGround()) || (rb.velocity.y <= 0 && rb.velocity.x <= 0))
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
            animator.SetBool("Walking", false);
            //animator.SetBool("Climbing", false);
            animator.SetBool("Idle", true);
        }
    }

    private void FallingAnimation()
    {
        if (rb.velocity.y < -0.2 && !isClimbing)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
            animator.SetBool("Walking", false);
            animator.SetBool("Climbing", false);
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Falling", false);
        }
    }
}
