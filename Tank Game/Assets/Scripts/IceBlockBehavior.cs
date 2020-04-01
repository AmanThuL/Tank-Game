using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlockBehavior : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] float maxSlideSpeed = 10;
    [SerializeField] [Range(0, 10)] float slideDeceleration = 1;
    [SerializeField] [Range(0, 10)] float ricochetSpeedReduction = 1;
    float currentSpeed;
    Vector3 direction;
    Rigidbody2D rb;
    private RaycastHit2D hit;
    public LayerMask collisionMask;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        currentSpeed -= slideDeceleration * Time.deltaTime;

        if (currentSpeed <= 0f)
        {
            return;
        }

        Vector3 movement = direction * currentSpeed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag[3]);
        if (collision.gameObject.tag[3] == 'B')
        {
            Vector3 collisionDirection = gameObject.transform.position - collision.gameObject.transform.position;
            if (Mathf.Abs(collisionDirection.x) > Mathf.Abs(collisionDirection.y))
            {
                direction = new Vector2(collisionDirection.x, 0f).normalized;
            }
            else
            {
                direction = new Vector2(0f, collisionDirection.y).normalized;
            }
            currentSpeed = maxSlideSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hit = Physics2D.Raycast(transform.position, direction, Time.deltaTime * currentSpeed + .1f, collisionMask);
        if (hit.collider != null)
        {
            Vector2 reflectDir = Vector2.Reflect(direction, hit.normal);
            direction = reflectDir;
            currentSpeed -= ricochetSpeedReduction;
        }
    }
}
