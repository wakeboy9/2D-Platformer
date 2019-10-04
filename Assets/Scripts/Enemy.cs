using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Enemy : MonoBehaviour
{
    // Projectile
    public GameObject proj1;

    // Variables for health
    [HideInInspector]
    public int health;
    [HideInInspector]
    public bool dead = false;
    [HideInInspector]
    public bool isHurting;
    [HideInInspector]
    public bool invulnerable;
    [HideInInspector]
    public float hurtTimer;

    // How often to fire, in seconds
    public float fireRate;

    // Player ship's transform 
    [HideInInspector]
    public Transform target;

    // How to move the enemy
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public Vector3 direction;

    // Stay this far away from player ship when following
    [HideInInspector]
    public float minDistance;

    public void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.up = (target.position - transform.position);
        StartCoroutine(Shoot());
    }

    // Take damage after hit with player projectile, bounce on hitting wall
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("PlayerProjectile"))
        {
            if (!invulnerable)
            {
                Hurt();
            }
            Destroy(other.gameObject);
        }
    }

    // Take damage
    public void Hurt()
    {
        if (isHurting) {return;}

        isHurting = true;
        health--;

        // Destroy game obj and load next level if main enemy is killed
        if ((health <= 0) && gameObject.CompareTag("EnemyMain")){
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (health <= 0) {Destroy(gameObject);}

        StartCoroutine(HurtRoutine());
    }

    // Blink black and white, stay invulnerable for hurtTimer seconds
    public IEnumerator HurtRoutine()
    {
        float startTime = Time.time;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        while (startTime + hurtTimer > Time.time)
        {
            sr.color = Color.black;
            yield return new WaitForSeconds(.1f);
            sr.color = Color.white;
            yield return new WaitForSeconds(.1f);
        }
        isHurting = false;
    }

    // Move in direction chosen in ChangeDirection
    public void Move()
    {
        transform.position += (direction * speed * Time.deltaTime);
    }

    // Move towards the player
    public void Chase()
    {
        minDistance = 1.7f;
        if (Vector3.Distance(transform.position, target.position) >= minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public abstract IEnumerator Shoot();
}
