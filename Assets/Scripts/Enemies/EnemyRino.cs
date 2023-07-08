using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRino : Enemy
{
    [Header("Rinho")]
    [SerializeField] private float aggresiveSpeed;
    [SerializeField] private LayerMask whatToIgnore; // default, Enemy
    [SerializeField] private float shockTime;

    private bool isAggresive;
    private RaycastHit2D playerDetection;
    private float shockTimeCounter;

    protected override void Start()
    {
        base.Start();
        invencible= true;
    }

    protected override void Update()
    {
        base.Update();
        
        playerDetection = Physics2D.Raycast(
            wallCheck.position, 
            Vector2.right * facingDirection, 
            50, 
            ~whatToIgnore);
        Move();
    }

    protected override void AnimationController()
    {
        base.AnimationController();
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
