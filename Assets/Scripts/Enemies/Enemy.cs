using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Damage()
    {
        Debug.Log("uyss");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        Player player = collision.collider.GetComponent<Player>();
        if (player == null) return;

        player.KnockBack(transform);
      
    }
}
