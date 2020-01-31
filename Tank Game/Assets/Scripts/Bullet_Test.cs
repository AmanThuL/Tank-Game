using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //GameObject tank;

    [SerializeField] [Range (0,20)] private float speed;
    public Vector3 direction;
    [SerializeField] private Vector3 velocity;
    public Vector3 position;
    private bool ricochet;

    // Start is called before the first frame update
    void Start()
    {
        ricochet = false;
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
        Debug.Log(collision.gameObject.tag +", "+ gameObject.tag);
        

        if (collision.gameObject.tag.Substring(0,3) != gameObject.tag.Substring(0,3))
        {
           
            //Collisions with tanks
            if (collision.gameObject.tag == "BluTank")
            {
                GameObject.Find("Game Manager").GetComponent<Manager>().KillBlueTank();
                DestroySelf();
                return;
            }
            if (collision.gameObject.tag == "RedTank")
            {
                GameObject.Find("Game Manager").GetComponent<Manager>().KillRedTank();
                DestroySelf();
                return;
            }

            //Collisions with walls
            if (ricochet != true)
            {
                ricochetBullet(collision);
                ricochet = true;
            }
            else
            {
                DestroySelf();
            }
            
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
        direction.Normalize();
        velocity = direction * speed;
        position = transform.position;
    }


    public void ricochetBullet(Collision2D col)
    {
        Vector3 v = Vector3.Reflect(transform.right, col.contacts[0].normal);
        float rot = 90 - Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, rot);
    }

}
