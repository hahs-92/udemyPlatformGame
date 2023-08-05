using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit_Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player player = collision?.GetComponent<Player>();
            if(player != null )
            {
                player.fruits++;
                Destroy(gameObject);
            }
        }
    }
}
