using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;
    public Rigidbody2D rb;

    [Header("Jump")]
    public float jumpForce;

    private float movingInput;
    private bool isGronded;
    private bool canDoubleJump = true;
    public LayerMask whatIsGround;
    public float groundCheckDistance;

    private void Update()
    {
        movingInput = Input.GetAxis("Horizontal");
        CollisionChecks();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton(); 
        }

        if(isGronded)
        {
            canDoubleJump= true;
        }

        Move();
    }

    private void JumpButton()
    {
        if (isGronded)
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
        isGronded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.
            DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
