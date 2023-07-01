using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapSawExtended : TrapSaw
{
    private bool goingFoward = true;
    protected override void Move()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, movePoints[movePointIndex].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePoints[movePointIndex].position) < 0.15f)
        {
            if(movePointIndex == 0)
            {
                Flip();
                goingFoward = true;
            }

            if(goingFoward)
            {
                movePointIndex++;
            } else
            {
                movePointIndex--;
            }
           
            if (movePointIndex >= movePoints.Length)
            {
                Flip();
                movePointIndex = movePoints.Length -1;
                goingFoward = false;
            }
        }
    }
}
