using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    private Animator anim;
    private void Awake()
    {
        PlayerManager.instance.respawnPoint = respawnPoint;
        PlayerManager.instance.PlayerRespawn();
        anim = GetComponent<Animator>();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(collision.transform.position.x > transform.position.x)
            {
                anim.SetTrigger("touch");
            }
        }
    }
}
