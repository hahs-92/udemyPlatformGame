using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator anim;


    private void Awake()
    {
        anim= GetComponent<Animator>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            anim.SetTrigger("activate");
            PlayerManager.instance.respawnPoint = transform;
        }
    }
}
