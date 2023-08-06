using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;
    private bool canMove;

    [Header("Jump")]
    [SerializeField] private float bufferJumpTime;
    [SerializeField] private float cayoteJumpTime;
    [SerializeField] private float groundCheckDistance;
    private float bufferJumpCounter;
    private float cayoteJumpCounter;
    private bool canHavecayoteJump;

    public float jumpForce;
    public float doubleJumpForce;
    private bool isGrounded;
    private bool canDoubleJump = true;
    private float defaultJumpForce;

    [Header("collision")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private Transform enemyCheck;
    [SerializeField] private float enemyCheckRadious;
    
    [Header("knockback")]
    [SerializeField] private Vector2 knockBackDirection;
    [SerializeField] private float knockBackTime;
    [SerializeField] private float knockBackProtectionTime;
    private CameraShakeEffects cameraFX;
    private bool isKnocked;
    private bool canBeKnocked = true;

    [Header("wall")]
    [SerializeField ]private float wallCheckDistance;
    public Vector2 wallJumpDirection;
    private bool isWallDetected;
    private bool canWallSlide;
    private bool isWallSliding;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;
    private float movingInput;
    private bool facingRight = true;
    private int facingDirection = 1;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb= GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        cameraFX = GetComponent<CameraShakeEffects>();
    }

    private void Start()
    {
        defaultJumpForce = jumpForce;
    }

    private void Update()
    {
        AnimationController();

        if (isKnocked) return;

        CollisionChecks();
        FlipController();
        InputChecks();

        CheckForEnemy();

        bufferJumpCounter -= Time.deltaTime;
        cayoteJumpCounter -= Time.deltaTime;

        if(isGrounded)
        {
            canDoubleJump = true;
            canMove = true;

            if(bufferJumpCounter > 0)
            {
                bufferJumpCounter = -1;
                Jump();
            }
            canDoubleJump = true;
        } else
        {
            if(canHavecayoteJump)
            {
                canHavecayoteJump= false;
                cayoteJumpCounter = cayoteJumpTime;
            }
        }

        if(canWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        } 

        Move();
    }

    private void CheckForEnemy()
    {
        Collider2D[] hitedColliders = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadious);

        foreach (var enemy in hitedColliders)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                Enemy newEnemy = enemy.GetComponent<Enemy>();

                if (newEnemy.invencible) return;

                if(rb.velocity.y < 0)
                {
                    newEnemy.Damage();
                    Jump();
                }
            }
        }
    }

    public void KnockBack(Transform damagePosition)
    {
        if(!canBeKnocked) return;

        if(GameManager.instance.difficulty > 1)
        {
            PlayerManager.instance.fruits--;
            if(PlayerManager.instance.fruits <= 0)
            {
                Destroy(gameObject);
            }

        }

        cameraFX.ScreenShake(-facingDirection);
        isKnocked = true;
        canBeKnocked = false;

        int hDirection = GetDamageDirection(damagePosition);    
        rb.velocity = new Vector2(knockBackDirection.x * hDirection, knockBackDirection.y);

        Invoke(nameof(CancelKnockBack), knockBackTime);
        Invoke(nameof(AllowKnockback), knockBackProtectionTime);
    }

    public void Push(float pushForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, pushForce);
    }

    private int GetDamageDirection(Transform damagePosition)
    {
        int hDirection = 0;
        if (transform.position.x > damagePosition.position.x)
        {
            hDirection = 1;
        }
        else if (transform.position.x < damagePosition.position.x)
        {
            hDirection = -1;
        }

        return hDirection;
    }

    private void InputChecks()
    {
        movingInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetAxisRaw("Vertical") < 0)
        {
            canWallSlide = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
        }
    }

    private void JumpButton()
    {
        if(!isGrounded)
        {
            bufferJumpCounter = bufferJumpTime;
        }


        if(isWallSliding)
        {
            WallJump();
            canDoubleJump= true;
        }
        else if(cayoteJumpCounter > 0 && rb.velocity.y <= 0)
        {
            Jump();
        }
        else if (isGrounded)
        {
            Jump();
        } else if(canDoubleJump)
        {
            canMove= true;
            canDoubleJump = false;
            jumpForce = doubleJumpForce;
            Jump();
            jumpForce= defaultJumpForce;
        }

        canWallSlide= false;
    }

    private void CancelKnockBack()
    {
        isKnocked = false;
    }

    private void AllowKnockback()
    {
        canBeKnocked = true;
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void WallJump()
    {
        canMove= false;
        rb.velocity = new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y);
    }

    private void Move()
    {
        if(canMove)
        {
            rb.velocity = new Vector2(moveSpeed * movingInput, rb.velocity.y);
        }
    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsWall);

        if(isWallDetected && rb.velocity.y < 0 )
        {
            canWallSlide = true;
        }

        if(!isWallDetected)
        {
            isWallSliding = false;
            canWallSlide = false;
        }
    }

    private void FlipController()
    {
        if(facingRight && rb.velocity.x < -.1f)
        {
            Flip();
        } else if(!facingRight && rb.velocity.x > .1f) 
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        sr.flipX = facingRight;
        facingRight = !facingRight;
        //transform.Rotate(0,180,0);
    }

    private void AnimationController()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isMoving", isMoving);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isWallDetected", isWallDetected);
        anim.SetBool("isKnocked", isKnocked);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.
            DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));

        Gizmos.color = Color.green;
        Gizmos.
            DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyCheck.position, enemyCheckRadious);
    }
}
