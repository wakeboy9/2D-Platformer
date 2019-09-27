using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{

    public float speed = 0.1f;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey (KeyCode.A))
        {
            gameObject.transform.Translate(Vector3.left * speed);

        }
        if (Input.GetKey (KeyCode.D))
        {
            gameObject.transform.Translate(Vector3.right * speed);
        }

        if (Input.GetKey (KeyCode.W))
        {
            gameObject.transform.Translate(Vector3.up * speed);
        }

        if (Input.GetKey (KeyCode.S))
        {
            gameObject.transform.Translate(Vector3.down * speed);
        }

        if (Input.GetKey (KeyCode.Space))
        {
            Fire();
        }
    }

    public void Fire()
    {
        GameObject p = (GameObject) (Instantiate (projectile, transform.position + transform.up * 1.5f, Quaternion.identity));
        p.GetComponent<Rigidbody2D>().AddForce(transform.up * 1000f);
    }
    
}
