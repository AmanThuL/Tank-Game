using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteAmmo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Blue_Tank(Clone)" || collision.gameObject.name == "Red_Tank(Clone)")
        {
            //Infinite ammo for limited time
            collision.gameObject.GetComponent<TankControls>().InfiniteAmmo();
            GameStats.Instance.changeTime = Time.time;
            Destroy(this.gameObject); 
        }
    }
}
