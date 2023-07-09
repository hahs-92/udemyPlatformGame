using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Trap
{
    private Rigidbody2D rb;

    private float xSpeed;
    private float ySpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        rb.velocity = new Vector2(xSpeed, ySpeed);
    }

    public void SetSpeed(float x, float y)
    {
        xSpeed = x;
        ySpeed = y;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Destroy(gameObject);
    }
}
