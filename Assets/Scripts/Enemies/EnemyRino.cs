using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRino : Enemy
{
    [Header("Rinho")]
    [SerializeField] private float aggresiveSpeed;
    [SerializeField] private float shockTime;

    private float shockTimeCounter;

    protected override void Start()
    {
        base.Start();
        invencible= true;
    }

    protected override void Update()
    {
        base.Update();
        Move();
    }

    protected override void AnimationController()
    {
        base.AnimationController();
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetBool("invencible", invencible);
    }

    private  void Move()
    {
        if (playerDetection.collider.GetComponent<Player>() != null) isAggresive = true;

        if (!isAggresive)
        {
            WalkAround();
        }
        else
        {
            rb.velocity = new Vector2(aggresiveSpeed * facingDirection, rb.velocity.y);

            if (wallDetected && invencible)
            {
                invencible = false;
                shockTimeCounter = shockTime;
            }

            if (shockTimeCounter <= 0 && !invencible)
            {
                invencible = true;
                Flip();
                isAggresive = false;
            }

            shockTimeCounter -= Time.deltaTime;
        }
    }
}
