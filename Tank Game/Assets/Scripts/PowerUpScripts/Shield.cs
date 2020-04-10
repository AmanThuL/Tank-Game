using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Blue_Tank(Clone)" || collision.gameObject.name == "Red_Tank(Clone)")
        {
            //Infinite ammo for limited time
            collision.gameObject.GetComponent<TankControls>().Shield();
            GameStats.Instance.changeTime = Time.time;
            Destroy(this.gameObject);
            GameStats.Instance.powerUpSpawned = false;
        }
    }
}
