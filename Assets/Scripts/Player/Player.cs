using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;

    [Header("Jump")]
    public float jumpForce;
    public float groundCheckDistance;
    private bool isGrounded;
    private bool canDoubleJump = true;

    [Header("collision")]
    public LayerMask whatIsGround;

    [Header("wall")]
    public float wallCheckDistance;
    private bool isWallDetected;
    private bool canWallSlide;
    private bool isWallSliding;

    private Rigidbody2D rb;
    private Animator anim;
    private float movingInput;
    private bool facingRight = true;
    private int facingDirection = 1;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
    }

    private void Update()
    {
        AnimationController();
        CollisionChecks();
        FlipController();
        InputChecks();

        if(isGrounded)
        {
            canDoubleJump = true;
        }

        if(canWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }

        Move();
    }

    private void InputChecks()
    {
        movingInput = Input.GetAxis("Horizontal");

        if(Input.GetAxis("Vertical") < 0)
        {
            canWallSlide = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
        }
    }

    private void JumpButton()
    {
        if (isGrounded)
        {
            Jump();
        } else if(canDoubleJump)
        {
            Jump();
            canDoubleJump = false;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveSpeed * movingInput, rb.velocity.y);
    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);

        if(isWallDetected && rb.velocity.y < 0 )
        {
            canWallSlide = true;
        }

        if(!isWallDetected)
        {
            canWallSlide = false;
        }
    }

    private void FlipController()
    {
        if(facingRight && movingInput < 0)
        {
            Flip();
        } else if(!facingRight && movingInput > 0) 
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }

    private void AnimationController()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isMoving", isMoving);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.
            DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.color = Color.green;

        Gizmos.
            DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));
    }
}
