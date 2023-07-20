using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{

    [Header("Bat")]
    [SerializeField] private Transform[] idlePoint;
    [SerializeField] private float checkRadious;
    [SerializeField] private LayerMask whatIsPlayer;

    private Vector2 destination;
    private bool canBeAggresive = true;
    private bool playerDetected;
    private float defaultSpeed;


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        invencible = true;
        defaultSpeed = speed;
        destination = idlePoint[0].position;
        transform.position = idlePoint[0].position;
    }

    protected override void Update()
    {
        base.Update();
        FlipControler();
        idleTimeCounter -= Time.deltaTime;

        if(idleTimeCounter > 0 )
        {
            return;
        }

        playerDetected = Physics2D.OverlapCircle(transform.position, checkRadious, whatIsPlayer);

        if (playerDetected && !isAggresive && canBeAggresive)
        {
            isAggresive = true;
            canBeAggresive = false;

            if(player != null)
            {
                destination = player.transform.position;
            } else
            {
                isAggresive = false;
                canBeAggresive= true;
            }
        }

        if(isAggresive)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if(Vector2.Distance(transform.position, destination) <.1f)
            {
                isAggresive = false;

                int i = Random.Range(0, idlePoint.Length);
                destination = idlePoint[i].position;
                speed = speed * .5f;
            }
        } else
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, destination) < .1f)
            {
                if(!canBeAggresive)
                {
                    idleTimeCounter = idleTime;
                    canBeAggresive= true;
                    speed = defaultSpeed;
                }
            }
        }
    }

    public override void Damage()
    {
        base.Damage();
        idleTimeCounter = 5;
    }

    protected override void AnimationController()
    {
        base.AnimationController();
        anim.SetFloat("speed", speed);
        anim.SetBool("canBeAggresive", canBeAggresive);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, checkRadious);
    }

    

    private void FlipControler()
    {
        if (player == null) return;
        if (facingDirection == -1 && transform.position.x < destination.x)
        {
            Flip();
        }
        else if (facingDirection == 1 && transform.position.x > destination.x)
        {
            Flip();
        }
    }

}
