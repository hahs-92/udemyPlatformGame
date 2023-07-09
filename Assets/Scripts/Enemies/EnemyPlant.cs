using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlant : Enemy
{
    [Header("Plant")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool rightDirection;
   

    protected override void Start()
    {
        base.Start();

        if(rightDirection)
        {
            Flip();
        }
    }

    protected override void Update()
    {
        base.Update();
        idleTimeCounter -= Time.deltaTime;

        bool playerDetected = playerDetection.collider?.GetComponent<Player>() != null;
        if(idleTimeCounter < 0 && playerDetected)
        {
            idleTimeCounter = idleTime;
            anim.SetTrigger("attack");
        }
    }

    // se llama en la animacion
    private void AttackEvent()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletPoint.transform.position, bulletPoint.transform.rotation);
        newBullet.GetComponent<Bullet>()?.SetSpeed(bulletSpeed * facingDirection, 0);
    }
}
