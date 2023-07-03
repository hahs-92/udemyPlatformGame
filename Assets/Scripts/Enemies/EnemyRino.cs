using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRino : Enemy
{
    [SerializeField] private float speed;
    [SerializeField] private float aggresiveSpeed;
    [SerializeField] private float idleTime;
    [SerializeField] private LayerMask whatToIgnore; // default, Enemy
    [SerializeField] private float shockTime;

    private float idleTimeCounter;
    private bool isAggresive;
    private RaycastHit2D playerDetection;
    private float shockTimeCounter;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        facingDirection = -1;
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

        if (playerDetection.collider.GetComponent<Player>() != null) isAggresive = true;
        
        if(!isAggresive)
        {
            if (idleTimeCounter <= 0)
                rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);
            else
                rb.velocity = Vector2.zero;

            idleTimeCounter -= Time.deltaTime;


            if (wallDetected || !groundDetected)
            {
                idleTimeCounter = idleTime;
                Flip();
            }
        } else
        {
            rb.velocity = new Vector2(aggresiveSpeed * facingDirection, rb.velocity.y);

            if(wallDetected && invencible)
            {
                invencible= false;
                shockTimeCounter = shockTime;
            }

            if(shockTimeCounter <= 0 && !invencible)
            {
                invencible = true;
                Flip();
                isAggresive = false;
            }

            shockTimeCounter -= Time.deltaTime;
        }
    }
}
