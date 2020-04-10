using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Blue_Tank(Clone)" || collision.gameObject.name == "Red_Tank(Clone)")
        {
            //Spread shot for limited time
            collision.gameObject.GetComponent<TankControls>().SpreadShot();
            GameStats.Instance.changeTime = Time.time;
            GameStats.Instance.powerUpSpawned = false;
            Destroy(this.gameObject);
        }
    }
}
