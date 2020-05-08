using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Blue_Tank(Clone)" || collision.gameObject.name == "Red_Tank(Clone)")
        {
            AudioManager.Instance.PlaySound("player/pickuppowerup");
            //Infinite ammo for limited time
            collision.gameObject.GetComponent<TankControls>().DoubleShot();
            GameStats.Instance.changeTime = Time.time;
            GameStats.Instance.powerUpSpawned = false;
            Destroy(this.gameObject);
        }
    }
}
