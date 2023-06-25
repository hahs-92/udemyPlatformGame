using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;

    [Header("Jump")]
    public float jumpForce;

    [Header("collision")]
    public LayerMask whatIsGround;
    public float groundCheckDistance;

    private Rigidbody2D rb;
    private Animator anim;
    private float movingInput;
    private bool isGrounded;
    private bool canDoubleJump = true;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
    }

    private void Update()
    {
        movingInput = Input.GetAxis("Horizontal");
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
    }
}
