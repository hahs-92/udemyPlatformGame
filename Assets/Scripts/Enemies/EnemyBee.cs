using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBee : Enemy
{
    [Header("Bee")]
    [SerializeField] private Transform[] idlePoint;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private float checkRadious;
    [SerializeField] private float yOffset;

    private Transform player;
    private bool playerDetected;
    private float defaultSpeed;
    private int idlePointIndex;

    [Header("Bullet Bee")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private float bulletSpeed;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.Find("Player").transform;
    }

    protected override void Start()
    {
        base.Start();
        defaultSpeed = speed;
    }

    protected override void Update()
    {
        base.Update();
        bool idle = idleTimeCounter > 0;
        idleTimeCounter -= Time.deltaTime;
        anim.SetBool("idle", idle);

        if (idle)
        {
            return;
        }

        playerDetected = Physics2D.OverlapCircle(playerCheck.position, checkRadious, whatIsPlayer);

        if(playerDetected && !isAggresive)
        {
            isAggresive = true;
            speed *= 1.5f;
        }

        if(!isAggresive)
        {
            transform.position = Vector2.MoveTowards(transform.position, idlePoint[idlePointIndex].position, speed * Time.deltaTime);

            if(Vector2.Distance(transform.position, idlePoint[idlePointIndex].position) < .1f)
            {
                idlePointIndex++;

                if(idlePointIndex == idlePoint.Length)
                {
                    idlePointIndex = 0;
                }
            }
        } else
        {
            Vector2 newPosition = new Vector2(player.transform.position.x, player.transform.position.y + yOffset);
            transform.position = Vector2.MoveTowards(transform.position,newPosition, speed * Time.deltaTime);

            float xDifference = transform.position.x - player.transform.position.x;

            if(Mathf.Abs(xDifference) < .15f)
            {
                    
                anim.SetTrigger("attack");
            } 
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(playerCheck.position, checkRadious);
    }

    private void AttackEvent()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletPoint.transform.position, bulletPoint.transform.rotation);
        newBullet.GetComponent<Bullet>()?.SetSpeed(0, -speed);
        speed = defaultSpeed;

        idleTimeCounter = idleTime;
        isAggresive = false;
    }
}
