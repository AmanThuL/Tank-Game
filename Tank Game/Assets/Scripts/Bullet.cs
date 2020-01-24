using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //GameObject tank;

    [SerializeField] [Range (0,3)] float speed;
    public Vector3 direction;
    [SerializeField] public Vector3 velocity;
    public Vector3 position;

    // Start is called before the first frame update
    void Start()
    {

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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "Blue")
    //    {
    //        GameObject.Find("Game Manager").GetComponent<Manager>().KillBlueTank();
    //    }
    //    if(collision.gameObject.tag == "RedTank")
    //    {
    //        GameObject.Find("Game Manager").GetComponent<Manager>().KillRedTank();
    //    }

    //    DestroySelf();
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != gameObject.tag)
        {
            if (collision.gameObject.tag == "Blue")
            {
                GameObject.Find("Game Manager").GetComponent<Manager>().KillBlueTank();
            }
            if (collision.gameObject.tag == "Red")
            {
                GameObject.Find("Game Manager").GetComponent<Manager>().KillRedTank();
            }

            DestroySelf();
        }
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

    public void Initialize( Vector3 dir)
    {
        direction = dir;
        speed = .2f;
        direction.Normalize();
        velocity = direction * speed;
    }
}
