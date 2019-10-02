using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyController : Enemy
{
    public float rotationSpeed;

    // Start is called before the first frame update
    new void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(Shoot());

        hurtTimer = 0.3f;
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
    }

    // Fire 4 projectile every fireRate seconds, set rotation to be the local cardinal rotation
    public override IEnumerator Shoot()
    {
        while (!dead && target != null)
        {
            Instantiate(proj1, transform.position + (transform.up * 1.1f), Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z));
            Instantiate(proj1, transform.position - (transform.up * 1.1f), Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z + 180));
            Instantiate(proj1, transform.position + (transform.right * 1.1f), Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z + 270));
            Instantiate(proj1, transform.position - (transform.right * 1.1f), Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z + 90));

            yield return new WaitForSeconds(fireRate);
        }
    }

    void Turn() {
        transform.RotateAround(transform.position,  transform.forward, rotationSpeed);
    }
}
