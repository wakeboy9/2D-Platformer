using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{

    public float speed = 0.1f;
    public GameObject projectile;

    public float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey (KeyCode.A))
        {
            transform.Translate(Vector3.left * speed);

        }
        if (Input.GetKey (KeyCode.D))
        {
            transform.Translate(Vector3.right * speed);
        }

        if (Input.GetKey (KeyCode.W))
        {
            transform.Translate(Vector3.up * speed);
        }

        if (Input.GetKey (KeyCode.S))
        {
            transform.Translate(Vector3.down * speed);
        }

        if (Input.GetKey (KeyCode.Space))
        {
            StartCoroutine(Fire());
        }
    }

    // public void Fire()
    // {
    //     GameObject p = (GameObject) (Instantiate (projectile, transform.position + transform.up * 1.5f, Quaternion.identity));
    //     p.GetComponent<Rigidbody2D>().AddForce(transform.up * 1000f);
    // }

    IEnumerator Fire() {
        Instantiate(projectile, transform.position + (transform.forward * 1.1f), Quaternion.identity);
        yield return new WaitForSeconds(fireRate);
    }
    
}
