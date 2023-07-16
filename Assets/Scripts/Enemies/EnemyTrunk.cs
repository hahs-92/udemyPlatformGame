using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTrunk : Enemy
{
    [Header("Trunk")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform groundBehindCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private float retreatTime;
    [SerializeField] private bool wallBehind;
    [SerializeField] private bool groundBehind;

    private bool playerDetected;
    private float attackCoolDownCounter;
    private float retreatTimeCounter;

    [Header("Bullet Trunk")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float attackCoolDown;



    protected override void Update()
    {
        base.Update();
        attackCoolDownCounter -= Time.deltaTime;
        retreatTimeCounter -= Time.deltaTime;

        if(!canMove)
        {
            rb.velocity = Vector3.zero;
        }

        if(playerDetected && retreatTimeCounter < 0)
        {
            retreatTimeCounter = retreatTime;
        }

        if(playerDetection.collider.GetComponent<Player>() != null )
        {
            if(attackCoolDownCounter < 0)
            {
                attackCoolDownCounter = attackCoolDown;
                anim.SetTrigger("attack");
                canMove= false;
            } else if(playerDetection.distance < 3)
            {
                MoveBackwards(1.5f);
            }
        } else
        {
            if(retreatTimeCounter > 0)
            {
                MoveBackwards(4);
            }
            else
            {
                WalkAround();
            }
        }
    }


    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        playerDetected = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);

      
        groundBehind = Physics2D.Raycast(groundBehindCheck.position,
            Vector2.down, groundCheckDistance, whatIsGround);
        
        wallBehind = Physics2D.Raycast(
                wallCheck.position,
                Vector2.right * (-facingDirection + 1) , wallCheckDistance, whatIsGround);

    }

    protected override void AnimationController()
    {
        base.AnimationController();
        anim.SetFloat("xVelocity", rb.velocity.x);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, checkRadius);
        Gizmos.DrawLine(groundBehindCheck.position, new Vector2(groundBehindCheck.position.x, groundBehindCheck.position.y - groundCheckDistance));
    }

    private void MoveBackwards(float multiplier)
    {
        if (wallBehind) return;
        if(!groundBehind) return;

        rb.velocity = new Vector2(speed* multiplier * -facingDirection, rb.velocity.y);
    }

    private void AttackEvent()
    {
        Debug.Log("attack trunk");
        GameObject newBullet = Instantiate(bulletPrefab, bulletPoint.transform.position, bulletPoint.transform.rotation);
        newBullet.GetComponent<Bullet>()?.SetSpeed(bulletSpeed * facingDirection, 0);
    }

    private void ReturnMovementEvent()
    {
        canMove = true;
    }
}
