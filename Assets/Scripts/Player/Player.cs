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

    private void Update()
    {
        movingInput = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        rb.velocity = new Vector2(moveSpeed * movingInput, rb.velocity.y);
    }
}
