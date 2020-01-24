using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject tank;

    float speed;
    public Vector3 direction;
    public Vector3 velocity;
    public Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        tank = GameObject.Find("Blue_Tank");
        direction = tank.GetComponent<Vehicle>().direction;
        position = tank.GetComponent<Vehicle>().vehiclePosition + direction * .35f;
        speed = tank.GetComponent<Vehicle>().maxSpeed * .8f;
        transform.rotation = Quaternion.Euler(direction);

        velocity = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBullet();
    }


    void MoveBullet()
    {
        position += velocity;
        transform.position = position;
    }
}
