using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    private float movingInput;

    private void Update()
    {
        movingInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveSpeed * movingInput, rb.velocity.y);
    }
}
