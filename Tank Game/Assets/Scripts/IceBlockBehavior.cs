using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlockBehavior : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] float maxSlideSpeed = 10;
    [SerializeField] [Range(0, 10)] float slideDeceleration = 1;
    [SerializeField] [Range(0, 10)] float ricochetSpeedReduction = 1;
    public float currentSpeed;
    public Vector3 direction;
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

        rb.velocity = Vector2.zero;
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

        currentSpeed = 0;
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //currentSpeed = 0;
    }
}
