using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTankControls : MonoBehaviour
{
    // Movement
    [Header("Movement")]
    [SerializeField] private Vector3 acceleration;
    [SerializeField] [Range(0, 5)] private float accelRate;
    [SerializeField] [Range(0, 1)] private float deceleration;
    [SerializeField] [Range(0, 20)] private float maxSpeed;

    [Header("Rotation")]
    [SerializeField] [Range(30, 200)] private float turnSpeed;
    //[SerializeField] private float angleOfRotation;

    //hold what the actual movement is of the tank
    [Header("Tank Properties")]
    public Vector3 tankPos;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 direction;

    private bool ifDecelerating = false;

    [SerializeField] KeyCode moveUp;
    [SerializeField] KeyCode moveDown;
    [SerializeField] KeyCode moveLeft;
    [SerializeField] KeyCode moveRight;
    [SerializeField] KeyCode shoot;


    public Rigidbody2D rb;

    //Bullet fire rate
    public GameObject bullet;
    float fireRate = .4f;
    float nextFire = 0f;

    // start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        tankPos = transform.position;
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
    protected void SetTransform()
    {
        //update info
        transform.rotation = Quaternion.Euler(0f,0f,Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg);
        transform.position += velocity * Time.deltaTime;
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
            //angleOfRotation += turnSpeed * Time.deltaTime;
            direction = Quaternion.Euler(0, 0, turnSpeed * Time.deltaTime) * direction;
        }
        if (Input.GetKey(moveRight))
        {
            //angleOfRotation -= turnSpeed * Time.deltaTime;
            direction = Quaternion.Euler(0, 0, -turnSpeed * Time.deltaTime) * direction;
        }

        direction.Normalize();
        SetTransform();
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
            velocity = (velocity.magnitude + acceleration.magnitude) *direction;

            //speed does not go over max speed
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }
        else if (Input.GetKey(moveDown))
        { 
            ifDecelerating = false;

            acceleration = accelRate * direction * Time.deltaTime;

            //backward movement
            //decrease speed
            velocity = -(velocity.magnitude + acceleration.magnitude) * direction;

            //speed does not go over max speed
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        }
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

            if (velocity.magnitude <= 0.05f)
            {
                velocity = Vector3.zero;

                ifDecelerating = false;
            }
            SetTransform();
        }
    }

    void ShootBullet()
    {
        GameObject tempBullet;
        if (Input.GetKey(shoot) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Debug.Log(angle);
            tempBullet = Instantiate(bullet, transform.position + direction * .35f, Quaternion.Euler(0, 0, angle));
            tempBullet.tag = gameObject.tag.Substring(0,3) + "Bullet";
            tempBullet.GetComponent<Bullet>().Initialize(direction);

        }
    }

}
