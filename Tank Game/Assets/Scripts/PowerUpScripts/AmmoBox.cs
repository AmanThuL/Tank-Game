using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{

    bool dead;
    float respawnTime;
    [SerializeField][Range(1,10)] float delay = 8f;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        Respawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Blue_Tank(Clone)" || collision.gameObject.name == "Red_Tank(Clone)")
        {
            AudioManager.Instance.PlaySound("player/pickupammo");
            collision.gameObject.GetComponent<TankControls>().AddAmmo();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            respawnTime = Time.time + delay;
            dead = true;
        }
    }
    

    void Respawn() {
        if (dead == true && Time.time > respawnTime)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            dead = false;
        }
    }
}
