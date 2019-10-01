using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{

    public float speed = 0.1f;
    public float rotationSpeed = 2;
    public GameObject projectile;

    public float fireRate;

    private bool isFiring;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        // Move
        if (Input.GetKey (KeyCode.A))
        {
            transform.position += (Vector3.left * speed);

        }
        if (Input.GetKey (KeyCode.D))
        {
            transform.position += (Vector3.right * speed);
        }

        if (Input.GetKey (KeyCode.W))
        {
            transform.position += (Vector3.up * speed);
        }

        if (Input.GetKey (KeyCode.S))
        {
            transform.position += (Vector3.down * speed);
        }

        // Rotate
        if (Input.GetKey (KeyCode.J)) {
            transform.Rotate(new Vector3(0, 0, 1) * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey (KeyCode.L)) {
            transform.Rotate(new Vector3(0, 0, -1) * rotationSpeed * Time.deltaTime);
        }
        
        // Fire
        if (Input.GetKey (KeyCode.Space) && !isFiring)
        {
            isFiring = true;
            StartCoroutine(Fire());
        }

        // Health
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    // Fire every amount of seconds, determined by fireRate
    IEnumerator Fire() {
        Instantiate(projectile, transform.position + (transform.up * 1.1f), transform.rotation);
        yield return new WaitForSeconds(fireRate);
        isFiring = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("EnemyProjectile")) {
            health--;
        }
    }
}
