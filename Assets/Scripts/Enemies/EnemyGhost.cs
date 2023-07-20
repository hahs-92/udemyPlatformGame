using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : Enemy
{
    [Header("Ghost")]
    [SerializeField] private float activeTime;

    private SpriteRenderer sr;
    private float activeTimeCounter = 4f;


    protected override void Awake()
    {
        base.Awake();
        sr = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        isAggresive = true;
    }

    protected override void Update()
    {
        base.Update();

        if (player == null)
        {
            anim.SetTrigger("desappear");
            return;
        }

        activeTimeCounter -= Time.deltaTime;
        idleTimeCounter -= Time.deltaTime;

        if(activeTimeCounter > 0)
        {
            transform.position = 
                Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
       
        if(activeTimeCounter < 0 && idleTimeCounter < 0 && isAggresive)
        {
            anim.SetTrigger("desappear");
            isAggresive= false;
            idleTimeCounter = idleTime;
        }

        if(activeTimeCounter < 0 && idleTimeCounter < 0 && !isAggresive)
        {
            ChoosePosition();
            anim.SetTrigger("appear");
            isAggresive= true;
            activeTimeCounter = activeTime;
        }

        if (player == null) return;

        if (facingDirection == -1 && transform.position.x < player.transform.position.x)
        {
            Flip();
        }
        else if (facingDirection == 1 && transform.position.x > player.transform.position.x)
        {
            Flip();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(isAggresive)
            base.OnTriggerEnter2D(collision);
    }

    public void Desappear()
    {
        //sr.enabled= false;
        sr.color = Color.clear;
    }

    public void Appear()
    {
        //sr.enabled = true;
        sr.color = Color.white;
    }

    private void ChoosePosition()
    {
        float yOffset = Random.Range(-7, 7);
        float xOffset = Random.Range(-7, 7);
        transform.position = new Vector2(player.transform.position.x + xOffset, player.transform.position.y + yOffset);
    }
}
