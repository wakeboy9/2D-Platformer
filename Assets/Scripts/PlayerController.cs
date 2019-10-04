using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController: MonoBehaviour
{
    private AudioSource myAudioSource;
    public AudioClip[] audioClips;
    private int randomInt;
    private AudioClip clip;
    public AudioClip laserSound;

    // How fast to move and rotate
    public float speed = 0.1f;
    public float rotationSpeed = 2;

    // Projectile prefab
    public GameObject projectile;

    // How often to fire in seconds
    public float fireRate;
    
    // Invulnerability time and health
    public float hurtTimer = 1;
    private int health;

    // Sprites for player ship damage
    public Sprite hurt1;
    public Sprite hurt2;

    // Child and child's sprite mask to show player ship damage
    private GameObject damageChild;
    private SpriteMask sm;

    // Current statuses for invulnerability and firing limit
    private bool isHurting;
    private bool isFiring;

    // Set bounds of level so ship doesn't clip into or bounce on walls
    private float maxX = 5.62f, minX = -5.6f;
    private float maxY = 3.97f, minY = -3.98f;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        randomInt = Random.Range(0, audioClips.Length);
        clip = audioClips[randomInt];
        myAudioSource.PlayOneShot(clip);

        health = 3;

        damageChild = new GameObject("Empty");
        damageChild.AddComponent<SpriteMask>();
        damageChild.transform.SetParent(transform);
        damageChild.transform.localPosition = Vector3.zero; 

        sm = damageChild.GetComponent<SpriteMask>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move

        if (Input.GetKey (KeyCode.D) && transform.position.x < maxX)
        {
            transform.position += (Vector3.right * speed);
        }
        if (Input.GetKey (KeyCode.A) && transform.position.x > minX)
        {
            transform.position += (Vector3.left * speed);
        }
        if (Input.GetKey (KeyCode.W) && transform.position.y < maxY)
        {
            transform.position += (Vector3.up * speed);
        }

        if (Input.GetKey (KeyCode.S) && transform.position.y > minY)
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
        AudioSource.PlayClipAtPoint(laserSound, transform.position);
        Instantiate(projectile, transform.position + (transform.up * 1.1f), transform.rotation);
        yield return new WaitForSeconds(fireRate);
        isFiring = false;
    }

    // Take damage on hit by enemy projectile
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("EnemyProjectile"))
        {
            Destroy(other.gameObject);
            Hurt();
        }
    }

    // Health and damage
    void Hurt()
    {
        if (isHurting) {return;}

        StartCoroutine(HurtRoutine());

        isHurting = true;
        health--;

        if (health <= 0) {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        // Change ship sprite mask for damage
        switch (health)
        {
            case 2:
                sm.sprite = hurt1;
                break;
            case 1:
                sm.sprite = hurt2;
                break;
        }
    }

    // Blink red and white, stay invulnerable for hurtTimer seconds
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