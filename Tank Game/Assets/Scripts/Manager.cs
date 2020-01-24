using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] GameObject BlueTank;
    [SerializeField] GameObject RedTank;

    [SerializeField] Vector3 blueSpawnPosition;
    [SerializeField] Vector3 redSpawnPosition;

    GameObject activeRedTank, activeBlueTank;

    [SerializeField] [Range(0, 5)] float SpawnDelay;
    float spawnTimerBlue = 0f;
    float spawnTimerRed = 0f;

    bool blueDead, redDead;
    bool blueAdvance = false, redAdvance = false;

    // Start is called before the first frame update
    void Start()
    {
        //spawn the red and blue tank
        RespawnBlueTank();
        RespawnRedTank();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRespawnTimer(Time.deltaTime);
    }

    /// <summary>
    /// Update the spawn timers of both tanks
    /// </summary>
    /// <param name="dt"></param>
    void UpdateRespawnTimer(float dt)
    {
        if (blueDead)
        {
            spawnTimerBlue += dt;
            if (spawnTimerBlue > SpawnDelay)
            {
                RespawnBlueTank();
            }

        }

        if(redDead)
        {
            spawnTimerRed += dt;
            if (spawnTimerRed > SpawnDelay)
            {
                RespawnRedTank();
            }
        }
    }
    /// <summary>
    /// respawn the blue players tank
    /// </summary>
    void RespawnBlueTank()
    {
        blueDead = false;
        activeBlueTank = GameObject.Instantiate(BlueTank);
        activeBlueTank.gameObject.transform.position = blueSpawnPosition;
    }

    /// <summary>
    /// respawn the red players tank
    /// </summary>
    void RespawnRedTank()
    {
        redDead = false;
        activeRedTank = GameObject.Instantiate(RedTank);
        activeRedTank.gameObject.transform.position = redSpawnPosition;
    }

    /// <summary>
    /// kill the blue tank
    /// </summary>
    public void KillBlueTank()
    {
        //begin the timer for the blue tank
        blueDead = true;
        spawnTimerBlue = 0f;
        //set which tank can advance to win
        blueAdvance = false;
        redAdvance = true;

        //destroy the blue tank
        GameObject.Destroy(activeBlueTank);
    }

    /// <summary>
    /// Kill the red tank
    /// </summary>
    public void KillRedTank()
    {
        //begin the timer for the red tank
        redDead = true;
        spawnTimerRed = 0f;
        //set which tank can advance to win
        blueAdvance = true;
        redAdvance = false;

        //destroy the red tank
        GameObject.Destroy(activeRedTank);
    }

}
