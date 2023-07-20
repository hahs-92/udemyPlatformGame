using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMusroom : Enemy
{
    protected override void Update()
    {
        base.Update();
        WalkAround();
    }

    protected override void AnimationController()
    {
        base.AnimationController();
        anim.SetFloat("xVelocity", rb.velocity.x);
    }
}
