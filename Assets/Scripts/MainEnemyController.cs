using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainEnemyController : Enemy
{ 
    public GameObject proj2; 

    private Scene currentScene;
    private string sceneName;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        hurtTimer = 0.5f;
        speed = 2f;
        health = 5;

        // Get scene name to change main enemy activity
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        // in level 2, enemy simply bounces around
        if (sceneName == "Level2" || sceneName == "Level6")
        {
            ChangeDirection();
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
        if (sceneName == "Level2" || sceneName == "Level6")
        {
            Move();
        }

        else if (sceneName == "Level3")
        {
            Chase();
        }
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

    // Adds a direction change when a wall is hit
    new void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);

        // when the enemy hits the wall, bounce off of it
        if (other.gameObject.tag == "Wall")
        {
            ChangeDirection();
        }
    }

    // make the enemy bounce off the wall by calculating new movement vector
    void ChangeDirection()
    {
        //create a random direction vector with the magnitude of 1, 
        direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
    }
}