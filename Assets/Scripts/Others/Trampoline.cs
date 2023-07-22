using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float pushForce = 20f;
    [SerializeField] private bool canBeUsed = true;

    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeUsed) return;

        if (collision.CompareTag("Player"))
        {
            canBeUsed = false;
            anim.SetTrigger("activate");
            collision.GetComponent<Player>()?.Push(pushForce);
            Debug.Log("PUSH¡¡");
        }
    }

    // event which is activate from anim
    private void CanUseAgain() => canBeUsed = true;
}
