using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        
        Player player = collision.GetComponent<Player>();
        if (player == null) return;
        
        
        if (player.transform.position.x > transform.position.x)
        {
            player.KnockBack(1);
        } else if(player.transform.position.x < transform.position.x)
        {
            player.KnockBack(-1);
        } else
        {
            player.KnockBack(0);
        }
    }
}
