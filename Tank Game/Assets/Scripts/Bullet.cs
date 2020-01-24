using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject tank;

    float speed;
    public Vector3 direction;
    [SerializeField] public Vector3 velocity;
    public Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        //tank = GameObject.Find("Blue_Tank");
        //direction = tank.GetComponent<BlueTankControls>().direction;
        //position = tank.GetComponent<BlueTankControls>().vehiclePosition + direction * .35f;
        //speed = tank.GetComponent<BlueTankControls>().maxSpeed * .8f;
        //transform.rotation = Quaternion.Euler(direction);

        //velocity = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBullet();
    }


    void MoveBullet()
    {
        position += velocity * Time.deltaTime;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "BlueTank")
        {
            GameObject.Find("Game Manager").GetComponent<Manager>().KillBlueTank();
        }
        if(collision.gameObject.tag == "RedTank")
        {
            GameObject.Find("Game Manager").GetComponent<Manager>().KillRedTank();
        }

        DestroySelf();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DestroySelf();
    }

    /// <summary>
    /// The Bullet Destroys itself
    /// </summary>
    void DestroySelf()
    {
        GameObject.Destroy(gameObject);
    }

    /// <summary>
    /// Bullet Destroyes itself after a delay
    /// </summary>
    /// <param name="delay">Float, the number of seconds before the bullet destroyes itself</param>
    void DestroySelf(float delay)
    {
        GameObject.Destroy(this, delay);
    }
}
