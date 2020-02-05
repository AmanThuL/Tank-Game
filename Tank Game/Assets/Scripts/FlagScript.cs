using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BluTank")
        {
            GameObject.Find("Game Manager").GetComponent<Manager>().BlueCaptureFlag();
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "RedTank")
        {
            GameObject.Find("Game Manager").GetComponent<Manager>().RedCaptureFlag();
            Destroy(gameObject);
        }
    }
}
