using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject BlueTank;
    [SerializeField] GameObject RedTank;

    [SerializeField] Vector3 blueSpawnPosition;
    [SerializeField] Vector3 redSpawnPosition;

    public GameObject activeRedTank, activeBlueTank;

    [SerializeField] [Range(0, 5)] float SpawnDelay;
    float spawnTimerBlue = 0f;
    float spawnTimerRed = 0f;

    bool blueDead, redDead;
    bool blueAdvance = false, redAdvance = false;

    [Header("Tanks Properties")]
    [SerializeField] [Range(0, 10)] private float accelRate;
    [SerializeField] [Range(0, 1)] private float deceleration;
    [SerializeField] [Range(0, 20)] private float maxSpeed;
    [SerializeField] [Range(0, 360)] private float turnSpeed;


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
        //if the blue tank is dead
        if (blueDead)
        {
            //update it's timer
            spawnTimerBlue += dt;
            //if the blue tank is ready to respawn
            if (spawnTimerBlue > SpawnDelay)
            {
                RespawnBlueTank();
            }

        }

        //if the red tank is dead
        if(redDead)
        {
            //update it's timer
            spawnTimerRed += dt;
            //check if the red tank is ready to respawn
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
        //the blue tank is no longer dead
        blueDead = false;
        //create a new blue tank
        activeBlueTank = GameObject.Instantiate(BlueTank);

        AssignTankProperties(activeBlueTank);
        //move the blue tank to it's spawn location
        activeBlueTank.gameObject.transform.position = blueSpawnPosition;
    }

    /// <summary>
    /// respawn the red players tank
    /// </summary>
    void RespawnRedTank()
    {
        //the red tank is no longer dead
        redDead = false;
        //create a new red tank
        activeRedTank = GameObject.Instantiate(RedTank);

        AssignTankProperties(activeRedTank);
        //move the red tank to it's spawn position
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

    private void AssignTankProperties(GameObject tank)
    {
        tank.GetComponent<BlueTankControls>().AccelRate = accelRate;
        tank.GetComponent<BlueTankControls>().Deceleration = deceleration;
        tank.GetComponent<BlueTankControls>().MaxSpeed = maxSpeed;
        tank.GetComponent<BlueTankControls>().TurnSpeed = turnSpeed;
    }

}
