using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBirth : Enemy
{
    [Header("Blue Birth")]
    [SerializeField] private float cellingDistance;
    [SerializeField] private float groundDistance;
    [SerializeField] private float flyUpForce;
    [SerializeField] private float flyDownForce;
    
    private float flyForce;
    private RaycastHit2D cellingDetected;


    protected override void Start()
    {
        base.Start();
        flyForce = flyUpForce;
    }

    protected override void Update()
    {
        base.Update();
        if(cellingDetected)
        {
            flyForce = flyDownForce;
        } else if(groundDetected)
        {
            flyForce = flyUpForce;
        }

        if(wallDetected)
        {
            Flip();
        }
    }

    public void FlyUpEvent()
    {
        rb.velocity = new Vector2(speed * facingDirection, flyForce);
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        cellingDetected = Physics2D.Raycast(transform.position, Vector2.up, groundDistance, whatIsGround);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundDistance));
        Gizmos.color= Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + cellingDistance));
    }
}
