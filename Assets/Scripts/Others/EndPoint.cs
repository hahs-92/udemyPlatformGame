using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("activate");
            Debug.Log("Win¡¡");

            GameManager.instance.SaveBestTime();
            GameManager.instance.SaveCollectedFruits();
            GameManager.instance.SaveLevelInfo();
        }
    }
}
