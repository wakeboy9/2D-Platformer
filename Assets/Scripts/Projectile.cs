using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//projectile script
//projectiles need a 2D colider set to trigger and a rigidbody 2D set to kinematic

public class Projectile : MonoBehaviour
{

    public float speed;
    public float lifeTime = 10;
    public bool invulnerable;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(KillAfterSeconds());
        
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }


    /// Destroys the projectile after some seconds
    IEnumerator KillAfterSeconds()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Wall") {
            Destroy(gameObject);
        }   
        if(other.CompareTag("PlayerProjectile") && !gameObject.CompareTag("PlayerProjectile")) {
            Destroy(other);
            if (!invulnerable)
            {
                Destroy(gameObject);
            }
        }
    }
}


