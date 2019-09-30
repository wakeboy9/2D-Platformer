using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//projectile script
//projectiles need a 2D colider set to trigger and a rigidbody 2D set to kinematic

public class Projectile : MonoBehaviour
{

    public float speed = 2;
    public float lifeTime = 10;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(KillAfterSeconds());
        
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.position * speed;
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    /// Destroys the projectile after some seconds
    IEnumerator KillAfterSeconds()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

}


