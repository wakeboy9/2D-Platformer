using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemyController : Enemy
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        minDistance = 1;
        speed = 1.4f;
        health = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // Aim at player ship
        if(target != null) {
            transform.up = (target.position - transform.position);
        }

        Chase();
    }

    // Fire projectile at enemy every fireRate seconds
    public override IEnumerator Shoot()
    {
        while (!dead && target != null)
        {
            Instantiate(proj1, transform.position + (transform.up * 1.1f), transform.rotation);

            yield return new WaitForSeconds(fireRate);
        }
    }
}
