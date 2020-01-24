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
        direction = tank.GetComponent<BlueTankControls>().direction;
        position = tank.GetComponent<BlueTankControls>().transform.position + direction * .35f;
        speed = .2f;
        direction.Normalize();
        //transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(direction.x, direction.y));

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
