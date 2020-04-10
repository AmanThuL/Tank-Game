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
    [SerializeField] Camera cam;

    //not yet implemented, for spawn full measure
    List<GameObject> spawns = new List<GameObject>();
    List<GameObject> activeSpawns = new List<GameObject>();
    bool hasCheckedFirstScreen = false;
    float spawnTimer;


    GameObject currentPowerup;

    List<GameObject> powerUps;

    // Start is called before the first frame update
    void Start()
    {
        currentPowerUpSpawnDelay = minPowerupSpawnDelay;
        spawnTimer = currentPowerUpSpawnDelay;
        powerUps = new List<GameObject>();

        if (GameStats.Instance.speedUp)
        {
            powerUps.Add(speedUp);
        }

        if (GameStats.Instance.infAmmo)
        {
            powerUps.Add(infAmmo);
        }

        if (GameStats.Instance.doubleShot){      powerUps.Add(doubleShot);}

        if (GameStats.Instance.shield) {         powerUps.Add(shield);}
        GameStats.Instance.powerUpSpawned = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasCheckedFirstScreen) { ChangeScreen(); hasCheckedFirstScreen = true; }
        SpawnPowerUp(Time.deltaTime);
    }

    void SpawnPowerUp(float dt)
    {
        if (!GameStats.Instance.powerUpSpawned){
            spawnTimer -= dt;
            if (spawnTimer < 0 )
            {
                if (spawns.Count > 0 && activeSpawns.Count > 0)
                {
                    //get the location at which to spawn the powerup
                    int spawnLocation = Random.Range(0, activeSpawns.Count);

                    //spawn the power up
                    currentPowerup = Instantiate
                        (
                        powerUps[Random.Range(0, powerUps.Count)],
                        new Vector3(activeSpawns[spawnLocation].transform.position.x, activeSpawns[spawnLocation].transform.position.y, 10),
                        Quaternion.identity
                        );

                    //tell the game we have spawned the powerup
                    GameStats.Instance.powerUpSpawned = true;
                }
                //reset the timer
                currentPowerUpSpawnDelay = Mathf.Min(currentPowerUpSpawnDelay + powerupSpawnDelayIncrement, maxPowerupSpawnDelay);
                spawnTimer = currentPowerUpSpawnDelay;
            }
        }
    }

    public void ChangeScreen()
    {
        if (currentPowerup != null)
        {
            Destroy(currentPowerup);
            GameStats.Instance.powerUpSpawned = false;
        }

        spawnTimer = currentPowerUpSpawnDelay;
        activeSpawns.Clear();
        for (int i = 0; i < spawns.Count; i++)
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(spawns[i].transform.position);
            bool onScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            if (onScreen)
            {
                activeSpawns.Add(spawns[i]);
            }
        }
        //Debug.Log(activeSpawns.Count);

    }

    public void addToSpawnLocations(GameObject spawn)
    {
        spawns.Add(spawn);
    }

}
