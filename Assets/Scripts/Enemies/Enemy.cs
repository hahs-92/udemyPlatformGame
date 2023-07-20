using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Trap
{
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatToIgnore; // default, Enemy
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected float speed;
    [SerializeField] protected float idleTime;
    
    protected Animator anim;
    protected int facingDirection = 1;
    protected Rigidbody2D rb;
    protected RaycastHit2D playerDetection;
    protected bool canMove = true;
    protected bool isAggresive;
    protected float idleTimeCounter;
    protected bool wallDetected;
    protected bool groundDetected;

    public bool invencible = true;


    protected void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        Debug.Log("Invisible");
    }

    protected void OnBecameVisible()
    {
        gameObject.SetActive(true);
        Debug.Log("visible");
    }


    protected virtual void Awake()
    {
        anim= GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        facingDirection = -1;

        if (wallCheck == null) wallCheck = transform;
        if (groundCheck == null) groundCheck = transform;
    }

    protected virtual void Update()
    {
        CollisionChecks();
        AnimationController();
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }


    public virtual void Damage()
    {
        if(!invencible)
        {
            Debug.Log("<color= blue> Damage</color>");
            anim.SetTrigger("gotHit");
            canMove= false;
        }
    }

    protected virtual void WalkAround()
    {
        if (idleTimeCounter <= 0 && canMove)
            rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);
        else
            rb.velocity = Vector2.zero;

        idleTimeCounter -= Time.deltaTime;

        if (wallDetected || !groundDetected)
        {
            idleTimeCounter = idleTime;
            Flip();
        }
    }

    protected virtual void AnimationController()
    {
       
    }

    protected virtual void Flip()
    {
        facingDirection = facingDirection * -1;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void CollisionChecks()
    {
        if(groundCheck != null)
        {
            groundDetected = Physics2D.Raycast(
                groundCheck.position, 
                Vector2.down, groundCheckDistance, whatIsGround);
        }

        if(wallCheck != null)
        {
            wallDetected = Physics2D.Raycast(
                wallCheck.position, 
                Vector2.right * facingDirection, wallCheckDistance, whatIsGround);

            playerDetection = Physics2D.Raycast(
                wallCheck.position,
                Vector2.right * facingDirection,
                50,
                ~whatToIgnore);
        }
    }

    protected virtual void OnDrawGizmos()
    {
        if(groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        }

        if(wallCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * facingDirection, wallCheck.position.y));

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + playerDetection.distance * facingDirection, wallCheck.position.y));
        }
    }
}
