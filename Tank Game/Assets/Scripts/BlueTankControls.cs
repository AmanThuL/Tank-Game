using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTankControls : MonoBehaviour
{
    //Sepearte speed and direction to maintain turning speed
    [SerializeField] private Vector3 acceleration;
    [SerializeField] private float accelRate = 30f;
    [SerializeField] [Range(0, 1)] private float deceleration;
    [SerializeField] [Range(0, 3)] private float maxSpeed = 10f;
    //[SerializeField]
    //private float velocity = 0f;
    [SerializeField]
    private float turnSpeed = 2.4f;

    //hold what the actual movement is of the tank
    [SerializeField]
    private Vector3 velocity;
    public Vector3 direction = new Vector3(1f, 0f, 0f);

    [SerializeField] KeyCode moveUp;
    [SerializeField] KeyCode moveDown;
    [SerializeField] KeyCode moveLeft;
    [SerializeField] KeyCode moveRight;

    private bool ifDecelerating = false;

    public Rigidbody2D rb;

    //Bullet fire rate
    public GameObject bullet;
    float fireRate = .4f;
    float nextFire = 0f;

    // start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
    }

    //get player input in update
    void Update()
    {
        Rotate();
        Move();
        Decelerate();
        ShootBullet();
    }

    /// <summary>
    /// Sets the rigidbody of the tank
    /// </summary>
    protected void UpdateRB()
    {
        //update info
        rb.MovePosition(transform.position + velocity);
        rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// Rotates the tank using A/D key
    /// </summary>
    private void Rotate()
    {
        //turn the tank
        //rotate using angles
        if (Input.GetKey(moveLeft))
        {
            direction += transform.up * turnSpeed * Time.deltaTime;
        }
        if (Input.GetKey(moveRight))
        {
            direction -= transform.up * turnSpeed * Time.deltaTime;
        }

        direction.Normalize();
    }

    /// <summary>
    /// The tank moves forward/backward
    /// </summary>
    private void Move()
    {
        //this grouping handles acceleration and deceleration
        if (Input.GetKey(moveUp))
        {
            ifDecelerating = false;

            acceleration = accelRate * direction * Time.deltaTime;

            //forward movement
            //increase speed
            velocity += acceleration;

            //speed does not go over max speed
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }
        else if (Input.GetKey(moveDown))
        { 
            ifDecelerating = false;

            acceleration = accelRate * direction * Time.deltaTime;

            //backward movement
            //decrease speed
            velocity -= acceleration;

            //speed does not go over max speed
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        }

        UpdateRB();
    }

    /// <summary>
    /// Decelerates the tank
    /// </summary>
    private void Decelerate()
    {
        if ((Input.GetKeyUp(moveUp) || Input.GetKeyUp(moveDown)) && velocity != Vector3.zero)
        {
            ifDecelerating = true;
        }

        if (ifDecelerating)
        {
            velocity *= deceleration;
            Debug.Log(velocity);
            if (velocity.magnitude <= 0.0001f)
            {
                velocity = Vector3.zero;

                ifDecelerating = false;
            }
            UpdateRB();
        }
    }

    void ShootBullet()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Debug.Log(angle);
            Instantiate(bullet, transform.position + direction * .35f, Quaternion.Euler(0, 0, angle));
        }
    }

}
