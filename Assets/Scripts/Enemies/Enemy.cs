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

    protected Animator anim;
    protected Rigidbody2D rb;
    protected int facingDirection = 1;
    protected bool wallDetected;
    protected bool groundDetected;


    protected virtual void Awake()
    {
        anim= GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        CollisionChecks();
    }


    public void Damage()
    {
        Debug.Log("<color= blue> Damage</color>");
    }

    protected virtual void Flip()
    {
        facingDirection = facingDirection * -1;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void CollisionChecks()
    {
        groundDetected = Physics2D.Raycast(
            groundCheck.position, 
            Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(
            wallCheck.position, 
            Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        Player player = collision.collider.GetComponent<Player>();
        if (player == null) return;

        player.KnockBack(transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheck.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));
    }
}
