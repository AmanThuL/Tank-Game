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
    [SerializeField] [Range(30, 100)] private float turnSpeed;
    private float angleOfRotation;

    //hold what the actual movement is of the tank
    [Header("Tank Properties")]
    public Vector3 tankPos;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 direction;

    private bool ifDecelerating = false;

    // start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        tankPos = transform.position;
        direction = Vector3.right;
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
    protected void SetTransform()
    {
        //update info
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);

        transform.position += velocity * Time.deltaTime;
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
            angleOfRotation += turnSpeed * Time.deltaTime;
            direction = Quaternion.Euler(0, 0, turnSpeed * Time.deltaTime) * direction;
        }
        if (Input.GetKey(KeyCode.D))
        {
            angleOfRotation -= turnSpeed * Time.deltaTime;
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
            if (velocity.magnitude <= 0.05f)
            {
                velocity = Vector3.zero;

                ifDecelerating = false;
            }
            SetTransform();
        }
    }
}
