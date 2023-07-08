using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRedish : Enemy
{
    [Header("Redish")]
    [SerializeField] private float cellingDistance;
    [SerializeField] private float groundDistance;
    [SerializeField] private float aggresiveTime;
    [SerializeField] private float flyForce;

    private float aggresiveTimeCounter;
    private RaycastHit2D groundBelowDetected;
    private bool groundAboveDetected;


    protected override void Update()
    {
        base.Update();
        aggresiveTimeCounter -= Time.deltaTime;

        if(aggresiveTimeCounter < 0 && !groundAboveDetected)
        {
            rb.gravityScale = 1;
            isAggresive = false;
        }    

        if(!isAggresive)
        {
            if(groundBelowDetected && !groundAboveDetected)
            {
                rb.velocity = new Vector2(0, flyForce);
            }
        } else
        {
            if(groundBelowDetected.distance <= 1.25f)
            WalkAround();
        }
    }

    protected override void AnimationController()
    {
        base.AnimationController();
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetBool("aggresive", isAggresive);
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        groundAboveDetected = Physics2D.Raycast(transform.position, Vector2.up, groundDistance, whatIsGround);
        groundBelowDetected = Physics2D.Raycast(transform.position, Vector2.down, cellingDistance, whatIsGround);
    }

    public override void Damage()
    {
        if(!isAggresive)
        {
            aggresiveTimeCounter = aggresiveTime;
            rb.gravityScale = 12;
            isAggresive = true;
        } else
        {
            base.Damage();
        }

    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + cellingDistance));
    }

}
