using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFireSwitcher : MonoBehaviour
{
    private Animator anim;
    private TrapFire trapFire;

    [SerializeField] private float countDown = 2;
    private float timeNotActive;

    private void Awake()
    {
        anim= GetComponent<Animator>();
        trapFire= GetComponentInChildren<TrapFire>();
    }

    private void Update()
    {
        countDown -= Time.deltaTime;   
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (countDown > 0) return;

        if(collision.CompareTag("Player"))
        {
            countDown = timeNotActive;
            anim.SetTrigger("pressed");
            trapFire.FireSwitchAfter(5);
        }
    }
}
