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
    private bool invulnerable;

    private Transform target;

    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private float velocity = 2f;
    private Vector2 direction;
    private Vector2 movementPerSecond;

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

        if (sceneName == "Level2")
        {
            latestDirectionChangeTime = 0f;
            calcuateNewMovementVector();
        }

        else if (sceneName == "Level3")
        {

        }

    }

    // Update is called once per frame
    void Update()
    {

        if(health <= 0) {
            dead = true;
            Destroy(gameObject);
        }

        transform.up = (target.position - transform.position);

        if (sceneName == "Level2")
        {
            transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
            transform.position.y + (movementPerSecond.y * Time.deltaTime));
        }
    }


    IEnumerator shoot() {
        while(!dead) {            
            Instantiate(proj1, transform.position + (transform.up * 1.1f), transform.rotation);

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


        // when the enemy hits the wall, bounce off of it
        if (other.gameObject.tag == "Wall")
        {
            calcuateNewMovementVector();
        }
    }

    // make the enemy bounce off the wall by calculating new movement vector
    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = direction * velocity;
    }


}
