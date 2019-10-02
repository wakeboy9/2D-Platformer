using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyController : MonoBehaviour
{
    private Scene currentScene;
    private string sceneName;

    public GameObject proj1; // Undestroyable projectile
    public GameObject proj2; // Destroyable projectile

    public float fireRate; // How often to fire, in seconds

    private int health;
    private bool dead = false;
    private bool isHurting;
    private bool invulnerable;
    private float hurtTimer = 0.5f;

    private Transform target;

    private float velocity = 2f;
    private Vector2 direction;
    private Vector2 movementPerSecond;

    private int minDistance = 2;

    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        invulnerable = false;

        currentScene = SceneManager.GetActiveScene();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.up = (target.position - transform.position);
        StartCoroutine(shoot());

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

        if (health <= 0)
        {
            dead = true;
            Destroy(gameObject);
        }

        transform.up = (target.position - transform.position);

        if (sceneName == "Level2")
        {
            Move();
        }

        else if (sceneName == "Level3")
        {
            Chase();
        }
    }


    IEnumerator shoot()
    {
        while (!dead)
        {
            Instantiate(proj1, transform.position + (transform.up * 1.1f), transform.rotation);

            yield return new WaitForSeconds(fireRate);
        }

    }

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

    void Hurt()
    {
        if (isHurting)
        {
            return;
        }

        isHurting = true;
        health--;

        // Health
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        StartCoroutine(HurtRoutine());
    }

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

    void Move()
    {
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));
    }

    void Chase()
    {
        if (Vector3.Distance(transform.position, target.position) >= minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, velocity * Time.deltaTime);
        }

    }

}
