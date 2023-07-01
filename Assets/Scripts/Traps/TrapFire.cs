using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFire : Trap
{
    private Animator anim;
    public bool isWorking;
    public float repeatRate;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if(transform.parent == null)
        {
            InvokeRepeating(nameof(FireSwitch), 0, repeatRate);
        }
    }

    private void Update()
    {
        anim.SetBool("isWorking", isWorking);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(isWorking)
        {
            base.OnTriggerEnter2D(collision);
        }
    }

    public void FireSwitch()
    {
        isWorking = !isWorking;
    }

    public void FireSwitchAfter(float seconds)
    {
        CancelInvoke();
        isWorking = false;
        Invoke(nameof(FireSwitch), seconds);
    }
}
