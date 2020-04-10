﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public GameObject speedUp;
    public GameObject infAmmo;

    List<GameObject> powerUps;

    // Start is called before the first frame update
    void Start()
    {
        powerUps = new List<GameObject>();

        if (GameStats.Instance.speedUp)
        {
            powerUps.Add(speedUp);
        }

        if (GameStats.Instance.infAmmo)
        {
            powerUps.Add(infAmmo);
        }

        //Spawn random enabled powerUp
        Instantiate(powerUps[Random.Range(0, powerUps.Count)], new Vector2(0,2), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        spawnPowerUp();
    }

    void spawnPowerUp()
    {
        if (GameStats.Instance.changeTime != 0 && Time.time > GameStats.Instance.changeTime + 10)
        {
            Instantiate(powerUps[Random.Range(0, powerUps.Count)], new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 10), Quaternion.identity);
            GameStats.Instance.changeTime = 0;
        }
    }

}
