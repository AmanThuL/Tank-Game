using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public GameObject speedUp, infAmmo, shield, doubleShot;
    [SerializeField] [Range(0f, 20f)] float maxPowerupSpawnDelay = 15.0f;
    [SerializeField] [Range(0f, 20f)] float minPowerupSpawnDelay = 10.0f;
    [SerializeField] [Range(0f, 2f)] float powerupSpawnDelayIncrement = .2f;
    float currentPowerUpSpawnDelay;
    [SerializeField] Vector2 spawnOffset = new Vector2(0,2);
    GameObject currentPowerup;

    List<GameObject> powerUps;

    // Start is called before the first frame update
    void Start()
    {
        currentPowerUpSpawnDelay = minPowerupSpawnDelay;

        powerUps = new List<GameObject>();

        if (GameStats.speedUp){         powerUps.Add(speedUp);}

        if (GameStats.infAmmo){         powerUps.Add(infAmmo);}

        if (GameStats.doubleShot){      powerUps.Add(doubleShot);}

        if (GameStats.shield) {         powerUps.Add(shield);}

        //Spawn random enabled powerUp
        Instantiate(powerUps[Random.Range(0, powerUps.Count)], spawnOffset, Quaternion.identity);
        GameStats.powerUpSpawned = true;
    }

    // Update is called once per frame
    void Update()
    {
        spawnPowerUp();
    }

    void spawnPowerUp()
    {
        
        if (GameStats.changeTime != 0 && Time.time > GameStats.changeTime + currentPowerUpSpawnDelay && !GameStats.powerUpSpawned)
        {
            currentPowerup = Instantiate
                (
                powerUps[Random.Range(0, powerUps.Count)], 
                new Vector3(Camera.main.transform.position.x + GameStats.currScreenIndex + Random.Range(-1, 2) * spawnOffset.x, Camera.main.transform.position.y + Random.Range(-1,2) * spawnOffset.y, 10),
                Quaternion.identity
                );
            GameStats.changeTime = 0;
            GameStats.powerUpSpawned = true;
            currentPowerUpSpawnDelay = Mathf.Min(currentPowerUpSpawnDelay + powerupSpawnDelayIncrement, maxPowerupSpawnDelay);
            
        }
    }

    public void changeScreen()
    {
        if (currentPowerup != null)
        {
            GameObject.Destroy(currentPowerup);
            GameStats.powerUpSpawned = false;
        }
    }

}
