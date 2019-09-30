using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//projectile script
//projectiles need a 2D colider set to trigger and a rigidbody 2D set to kinematic

public class Projectile : MonoBehaviour
{

    public float speed = 2;
    public float lifeTime = 10;
    public Vector2 direction = new Vector2(0, 1);


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(KillAfterSeconds(lifeTime));
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
    }


    /// Destroys the projectile after some seconds
    IEnumerator KillAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

}


