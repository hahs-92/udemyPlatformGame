using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;

    [Header("Jump")]
    public float jumpForce;
    private bool isGrounded;
    private bool canDoubleJump = true;

    [Header("collision")]
    public LayerMask whatIsGround;
    public float groundCheckDistance;

    [Header("walk")]
    private bool isWallChecked;
    public float wallCheckDistance;

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
        movingInput = Input.GetAxis("Horizontal");
        FlipController();
        AnimationController();
        CollisionChecks();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton(); 
        }

        if(isGrounded)
        {
            canDoubleJump= true;
        }

        Move();
    }

    private void JumpButton()
    {
        if (isGrounded)
        {
            Jump();
        } else if(canDoubleJump)
        {
            Jump();
            canDoubleJump= false;
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
        isWallChecked = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
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
        
        Gizmos.
            DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));
    }
}
