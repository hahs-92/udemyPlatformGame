using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected float speed;
    [SerializeField] protected float idleTime;
    
    protected bool canMove = true;
    protected bool isAggresive;
    protected float idleTimeCounter;
    protected Animator anim;
    protected Rigidbody2D rb;
    protected int facingDirection = 1;
    protected bool wallDetected;
    protected bool groundDetected;

    public bool invencible = true;


    protected virtual void Awake()
    {
        anim= GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        facingDirection = -1;
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
        }
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Player player = collision.GetComponent<Player>();
        if (player == null) return;

        player.KnockBack(transform);
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
        }
    }
}
