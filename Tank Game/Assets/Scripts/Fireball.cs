using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball: MonoBehaviour
{

    [SerializeField] [Range (0f,10f)]float speed = 1.0f;
    public Vector3[] endpoints = { new Vector3(), new Vector3(), new Vector3(), new Vector3()};
    Vector3 prevPos;
    Vector3 nextPos;
    float timetoNextPos;
    float t = 0f;
    int intNextPos, intLastPos;

    // Start is called before the first frame update
    void Start()
    {
        intNextPos = 0;
        intLastPos = -1;
        updateNextPos();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        if (t < timetoNextPos)
        {
            float percenttime = t / timetoNextPos;
            transform.position = Vector3.Lerp(prevPos, nextPos, percenttime);
        }
        else
        {   
            transform.position = nextPos;
            updateNextPos();
        }

    }

    void updateNextPos()
    {
        intNextPos++; intLastPos++;
        if (intNextPos >= endpoints.Length) { intNextPos = 0; }
        if (intLastPos >= endpoints.Length) { intLastPos = 0; }

        nextPos = endpoints[intNextPos];
        prevPos = endpoints[intLastPos];
        timetoNextPos = Vector3.Distance(nextPos, prevPos) / speed;
        t = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collisions with tanks
        if (collision.gameObject.tag == "BluTank")
        {
            GameObject.Find("Game Manager").GetComponent<Manager>().KillBlueTank();
            return;
        }
        if (collision.gameObject.tag == "RedTank")
        {
            GameObject.Find("Game Manager").GetComponent<Manager>().KillRedTank();
            return;
        }
    }
}
