using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject proj1; // Undestroyable projectile
    public GameObject proj2; // Destroyable projectile

    public float fireRate; // How often to fire, in seconds

    private int health;
    private bool dead = false;
    private bool invulnerable;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        invulnerable = false;

        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(shoot());
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0) {
            Destroy(gameObject);
        }

        transform.up = -(target.position - transform.position);
    }

    IEnumerator shoot() {
        while(!dead) {            
            Instantiate(proj1, transform.position - (transform.up * 1.2f), Quaternion.identity);

            yield return new WaitForSeconds(fireRate);
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "PlayerProjectile") {
            if(!invulnerable) {
                health--;
            }
            Destroy(other.gameObject);
        } 
    }
}
