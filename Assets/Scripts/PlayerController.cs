using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{

    public float speed = 0.1f;
    public float rotationSpeed = 2;
    public GameObject projectile;

    public float fireRate;
    public float hurtTimer = 1;

    public Sprite hurt1;
    public Sprite hurt2;
    public Sprite hurt3;

    private bool isFiring;
    private bool isHurting;
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
    }

    // Fire every amount of seconds, determined by fireRate
    IEnumerator Fire() {
        Instantiate(projectile, transform.position + (transform.up * 1.1f), transform.rotation);
        yield return new WaitForSeconds(fireRate);
        isFiring = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("EnemyProjectile"))
        {
            Hurt();
            Destroy(other.gameObject);
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
            sr.color = Color.red;
            yield return new WaitForSeconds(.05f);
            sr.color = Color.white;
            yield return new WaitForSeconds(.05f);
        }
        isHurting = false;
    }

}
