using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlant : Enemy
{
    protected override void Update()
    {
        base.Update();
        idleTimeCounter -= Time.deltaTime;

        bool playerDetected = playerDetection.collider?.GetComponent<Player>() != null;
        if(idleTimeCounter < 0 && playerDetected)
        {
            idleTimeCounter = idleTime;
            anim.SetTrigger("attack");
        }
    }

    private void AttackEvent()
    {
        Debug.Log("Attack");
    }
}
