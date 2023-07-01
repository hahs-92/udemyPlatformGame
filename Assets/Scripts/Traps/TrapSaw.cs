using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSaw : Trap
{
    private Animator anim;
    [SerializeField] private bool isWorking;
    [SerializeField] private Transform[] movePoints;


    private void Awake()
    {
        anim= GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("isWorking", isWorking);
    }
}
