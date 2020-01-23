using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTankControls : MonoBehaviour
{
	//Sepearte speed and direction to maintain turning speed
	[SerializeField]
	float Acceleartion = 30f;
	[SerializeField]
	float maxspeed = 10f;

	public Rigidbody2D rb;
	float speed = 0f;

	//hold what the actual movement is of the tank
	Vector3 movement;


	private Vector3 direction = new Vector3(1f,0f,0f);
	
	[SerializeField]
	float turnSpeed = 2.4f;



	//currently unused
	//// start is called before the first frame update
	//void start()
	//{

	//}

	//get player input in update
	void Update()
    {

		//this grouping handles acceleration and deceleration
		if (Input.GetKey(KeyCode.W))
		{ //forward movement
			//increase speed
			speed += Acceleartion * Time.deltaTime;
			//speed does not go over max speed
			if (speed > maxspeed)
			{
				speed = maxspeed;
			}
		}
		else if (Input.GetKey(KeyCode.S))
		{ //backward movement
			//decrease speed
			speed -= Acceleartion * Time.deltaTime;
			//speed does not go over max speed
			if (speed < -maxspeed)
			{
				speed = -maxspeed;
			}
		}
		else
		{ //no movement / stopping
		  //decrease speed to stop
			if (speed > 0) { speed -= Acceleartion * Time.deltaTime; }
			else { speed += Acceleartion * Time.deltaTime; }

			
			if (speed > -0.001 && speed < 0.001)
			{
				speed = 0;
			}
			
		}

		//turn the tank
		//rotate using angles
		if (Input.GetKey(KeyCode.A))
		{
			direction = direction + transform.up * turnSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D))
		{
			direction = direction - transform.up * turnSpeed * Time.deltaTime;
		}

		direction.Normalize();
		

		//update info
		//calculate how far the tank should move
		movement = direction * speed * Time.deltaTime;

		rb.MovePosition(transform.position + movement);
		rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


    }

	
}
