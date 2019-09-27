using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//space ship script
//shoots projectiles, spins around and moves around the screen
//spaceship has a collider object with "trigger" checked so it can be hit by the enemy projectiles
//move with left analog controller, aim with right analog controller
//X to fire
//add desktop copntrollers

public class PlayerShip : MonoBehaviour
{

	//public float speed = 1;
	float speed = 50.0f;
    public float rateOfFire = 1;
    public GameObject playerProjectilePrefab;
    public GameObject explosionPrefab;
    public AudioClip laserSound;

    private float lastTimeFired = 0;
    private bool isDead = false;

    /// <summary>
    /// This is called by Unity every frame. It handles the ships movement and checks if it should fire
    /// </summary>
    void Update()
    {
        if (isDead) return;


		transform.Rotate(Vector3.up * speed * lastTimeFired.deltaTime);

		transform.Rotate(Vector3.right * speed * lastTimeFired.deltaTime);


		// move the ship left and right, depending on the horizontal input
		transform.position += Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        // if the fire button is pressed and we waited long enough since the last shot was fired, FIRE!
        if (Input.GetKey(KeyCode.Space) && (lastTimeFired + 1 / rateOfFire) < Time.time)
        {
            lastTimeFired = Time.time;
            FireTheLasers();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Bomb();
        }
    }

    /// <summary>
    /// This is called by Unity when the object collides with another object. It is only called, when both objects have any 2D collider attached, at least one of them is a trigger and at least of of
    /// the two colliding GameObjects has a Rigidbody2D attached.
    /// </summary>
    void OnCollisionEnter2D(Collision2D col)
    {

        // if the other object has the asteroid tag, the destroy the ship and restard the game
        if (col.gameObject.CompareTag("Asteroid"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            // load the active scene again, to restard the game. The GameManager will handle this for us. We use a slight delay to see the explosion.
            StartCoroutine(RestartTheGameAfterSeconds(1));
            // we can not destroy the spaceship since it needs to run the coroutine to restart the game.
            // instead, disable update (isDead = true) and remove the renderer to "hide" the object while we reload.
            isDead = true;
            Destroy(GetComponent<SpriteRenderer>());
        }
    }

    /// <summary>
    /// Helper function to include the shooting behavior.
    /// </summary>
    void FireTheLasers()
    {
        AudioSource.PlayClipAtPoint(laserSound, transform.position);

        // Shooting up
        Instantiate(projectilePrefab, transform.position + Vector3.up, Quaternion.identity);

        // Shooting left
        GameObject projectileObject = Instantiate(projectilePrefab, transform.position + Vector3.up + Vector3.left, Quaternion.identity) as GameObject;
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.direction.x = -0.5f;

        // Shooting right
        projectileObject = Instantiate(projectilePrefab, transform.position + Vector3.up + Vector3.right, Quaternion.identity) as GameObject;
        projectile = projectileObject.GetComponent<Projectile>();
        projectile.direction.x = 0.5f;
    }

    /// <summary>
    /// Kill all asteroids in the scene
    /// </summary>
    void Bomb()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject astr in asteroids)
        {
            astr.GetComponent<Asteroid>().OnHit();
        }
    }

    /// <summary>
    /// Wait seconds and reload current scene.
    /// </summary>
    IEnumerator RestartTheGameAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
