using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMusroom : Enemy
{
    [SerializeField] private float speed;
    [SerializeField] private float idleTime;
    private float idleTimeCounter;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        facingDirection = -1;
    }

    protected override void Update()
    {
        base.Update();
        if(idleTimeCounter <= 0) 
            rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);

        idleTimeCounter -= Time.deltaTime;

        if(wallDetected || !groundDetected)
        {
            idleTimeCounter = idleTime;
            Flip();
        }
    }
}
