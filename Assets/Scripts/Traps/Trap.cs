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
        
        player.KnockBack(transform);
    }
}
