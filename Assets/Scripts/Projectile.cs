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

    private Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();

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


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        
        else if(other.collider.CompareTag("PlayerProjectile") && !gameObject.CompareTag("PlayerProjectile")) 
        {
            Destroy(other.gameObject);

            if (!invulnerable) {Destroy(gameObject);}
        }

        else if(!other.collider.CompareTag("Player") && !gameObject.CompareTag("PlayerProjectile")) 
        {
            Physics2D.IgnoreCollision(other.collider, col);
        }

        else if(other.collider.CompareTag("EnemyProjectile") && gameObject.CompareTag("EnemyProjectile")) 
        {
            Physics2D.IgnoreCollision(other.collider, col);
        }
    }
}


