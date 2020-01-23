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
    private Vector3 direction = new Vector3(1f, 0f, 0f);

    private bool ifDecelerating = false;

    public Rigidbody2D rb;

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
        if (Input.GetKey(KeyCode.A))
        {
            direction += transform.up * turnSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
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
        if (Input.GetKey(KeyCode.W))
        {
            ifDecelerating = false;

            acceleration = accelRate * direction * Time.deltaTime;

            //forward movement
            //increase speed
            velocity += acceleration;

            //speed does not go over max speed
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
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
        if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) && velocity != Vector3.zero)
        {
            ifDecelerating = true;
        }

        if (ifDecelerating)
        {
            velocity *= deceleration;
            if (velocity.magnitude <= 0.0001f)
            {
                velocity = Vector3.zero;

                ifDecelerating = false;
            }
            UpdateRB();
        }
    }
}
