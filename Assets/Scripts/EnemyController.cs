using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyController : MonoBehaviour
{
    private Scene currentScene;
    private string sceneName;

    // Projectiles, proj2 is invulnerable
    public GameObject proj1; 
    public GameObject proj2; 

    // How often to fire, in seconds
    public float fireRate; 

    // Variables for health
    private int health;
    private bool dead = false;
    private bool isHurting;
    private bool invulnerable;
    private float hurtTimer = 0.5f;

    // Player ship's transform 
    private Transform target;

    // How to move the main enemy
    private float velocity = 2f;
    private Vector2 direction;
    private Vector2 movementPerSecond;

    // Stay this far away from player ship when following
    private int minDistance = 2;

    // Start is called before the first frame update
    void Start()
    {
        // Find, aim, and start shooting at start firing
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.up = (target.position - transform.position);
        StartCoroutine(shoot());

        // Set health
        health = 5;

        // Get scene name to change main enemy activity
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        // in level 2, enemy simply bounces around
        if (sceneName == "Level2")
        {
            ChangeDirection();
        }

        // in level 3, enemy follows the player
        else if (sceneName == "Level3")
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        // Aim at player ship
        if(target != null) {
            transform.up = (target.position - transform.position);
        }

        // Main enemy moves around on level 2,
        if (sceneName == "Level2")
        {
            Move();
        }

        // Main enemy chases enemy on level 3,
        else if (sceneName == "Level3")
        {
            Chase();
        }
    }

    // Fire projectile at enemy every fireRate seconds
    IEnumerator shoot()
    {
        while (!dead && target != null)
        {
            Instantiate(proj1, transform.position + (transform.up * 1.1f), transform.rotation);

            yield return new WaitForSeconds(fireRate);
        }

    }

    // Take damage after hit with player projectile, bounce on hitting wall
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            if (!invulnerable)
            {
                Hurt();
            }
            Destroy(other.gameObject);
        }


        // when the enemy hits the wall, bounce off of it
        if (other.gameObject.tag == "Wall")
        {
            ChangeDirection();
        }
    }

    // Take damage
    void Hurt()
    {
        if (isHurting) {return;}

        isHurting = true;
        health--;

        // Destroy game obj
        if (health <= 0) {Destroy(gameObject);}

        StartCoroutine(HurtRoutine());
    }

    // Blink black and white, stay invulnerable for hurtTimer seconds
    IEnumerator HurtRoutine()
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

    // make the enemy bounce off the wall by calculating new movement vector
    void ChangeDirection()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = direction * velocity;
    }

    // Move in direction chosen in ChangeDirection
    void Move()
    {
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));
    }

    // Move towards the player
    void Chase()
    {
        if (Vector3.Distance(transform.position, target.position) >= minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, velocity * Time.deltaTime);
        }

    }

}
