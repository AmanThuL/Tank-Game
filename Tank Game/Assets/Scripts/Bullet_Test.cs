using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Test : MonoBehaviour
{
    //GameObject tank;
    [SerializeField] GameObject explosion;
    [SerializeField] [Range (0,20)] private float speed;
    public Vector3 direction;
    [SerializeField] private Vector3 velocity;
    public Vector3 position;
    private bool ricochet;
    private Ray2D ray;
    private RaycastHit2D hit;
    public LayerMask collisionMask;

    float nextUpdate;
    float updateRate;

    [SerializeField]private int MAX_BOUNCES;
    private int currentBounces;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStats.isInputEnabled)
        {
            MoveBullet();

            if (currentBounces <= MAX_BOUNCES)
                RicochetBullet();
        }
    }


    void MoveBullet()
    {
        position += velocity * Time.deltaTime;
        transform.position = position;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag +", "+ gameObject.tag);

        if (collision.gameObject.tag.Substring(0,3) != gameObject.tag.Substring(0,3) || currentBounces > 1)
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

            if (collision.gameObject.tag == "Crate" || collision.gameObject.tag.Substring(3) == "Bullet")
            {
                Destroy(collision.gameObject);
                DestroySelf();
                return;
            }

            if(collision.gameObject.tag == "Bounds")
            {
                DestroySelf();
                return;
            }

            ////Collisions with walls
            //if (ricochet != true)
            //{
            //    RicochetBullet(collision.gameObject);
            //    ricochet = true;
            //}
            //else
            //{
            //    DestroySelf();
            //}

            if (currentBounces > MAX_BOUNCES) DestroySelf();
        }
    }

    /// <summary>
    /// The Bullet Destroys itself
    /// </summary>
    void DestroySelf()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        //Debug.Log(gameObject.tag.Substring(0, 3) + "Tank");
        GameObject.Find("Game Manager").GetComponent<Manager>().ReturnBullet(gameObject.tag);
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
        currentBounces = 1;
    }


    public void RicochetBullet()
    {
        hit = Physics2D.Raycast(transform.position, transform.right, Time.deltaTime * speed + .1f, collisionMask);
        if (hit.collider != null)
        {
            Vector2 reflectDir = Vector2.Reflect(direction, hit.normal);
            direction = reflectDir;
            velocity = direction * speed;
            float rot = Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, rot);

            // update bounced bool
            StartCoroutine(IncrementBounce());
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + direction * 5f);
    }

    private IEnumerator IncrementBounce()
    {
        yield return 0; // make it wait 1 frame
        currentBounces++;
    }
}
