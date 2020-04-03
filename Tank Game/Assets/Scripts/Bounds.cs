using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    [SerializeField] bool isRightBounds;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isRightBounds && collision.gameObject.tag == "BluTank")
        {
            Debug.Log("blue tank has moved");
            //if we are the right bounds we advance the blue tank
            GameObject.Find("Game Manager").GetComponent<Manager>().BluAdvance();
            GameStats.changeTime = Time.time;
        }
        else if(!isRightBounds &&  collision.gameObject.tag =="RedTank")
        {
            Debug.Log("red tank has moved");
            //if we are the left bounds we advance the red tank
            GameObject.Find("Game Manager").GetComponent<Manager>().RedAdvance();
            GameStats.changeTime = Time.time;
        }
    }
}
