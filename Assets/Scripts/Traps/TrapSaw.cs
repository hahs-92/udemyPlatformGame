using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSaw : Trap
{
    private Animator anim;
    [SerializeField] private float coolDown;
    [SerializeField] protected Transform[] movePoints;
    [SerializeField] protected float speed;

    private bool isWorking;
    private float coolDownTimer;
    protected int movePointIndex;


    private void Awake()
    {
        anim= GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("isWorking", isWorking);
        isWorking = coolDownTimer < 0;
        coolDownTimer -= Time.deltaTime;
        Move();
    }

    protected virtual void Move()
    {
        if (!isWorking) return;

        transform.position = 
            Vector3.MoveTowards(transform.position, movePoints[movePointIndex].position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, movePoints[movePointIndex].position) < 0.15f) {
            movePointIndex++;
            coolDownTimer = coolDown;
            Flip();

            if(movePointIndex >= movePoints.Length)
            {
                movePointIndex = 0;
            }
        }
    }

    protected void Flip()
    {
        transform.localScale = new Vector3(1, transform.localScale.y * -1);
    }
}
