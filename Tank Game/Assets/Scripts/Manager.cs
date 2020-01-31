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
    float currentpos = 0; //hold the position in game
    float targetpos = 0;
    bool screenMoving = false;
    [SerializeField] GameObject cam; //holds the camera
    [SerializeField] [Range (0,5)]int winBy = 2;
    [SerializeField] Vector3 ScreenWidth = new Vector3();
    [SerializeField] float screenMoveSpeed;

    [SerializeField] GameObject rightbounds;
    [SerializeField] GameObject leftbounds;

    GameObject currentLeftBounds;
    GameObject currentRightBounds;


    // Start is called before the first frame update
    void Start()
    {
        //spawn the red and blue tank
        RespawnBounds();
        RespawnBlueTank();
        RespawnRedTank();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRespawnTimer(Time.deltaTime);
        UpdateScreenMove(Time.deltaTime);
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
    /// Updates the screen movement
    /// </summary>
    void UpdateScreenMove(float dt)
    { 
        //do nothing if the screen is not moving
        if(!screenMoving)
        { return; }

        //if the screen is in place do nothing and stop the screen from moving
        if((targetpos <= currentpos + .01) && (targetpos >= currentpos - .01))
        {
            screenMoving = false;
            RespawnBounds();
            return;
        }

        //now we know the screen can move

        //get what direction we are moving in
        float dir = (targetpos - currentpos)/Mathf.Abs(targetpos - currentpos);
        float dist = Mathf.Abs(targetpos - currentpos);
        if (dist < screenMoveSpeed * dt)
        {
            currentpos = targetpos;
        }
        else
        {
            //move
            currentpos += screenMoveSpeed * dir * dt;
            //update the camera's position
        }
        cam.transform.position = new Vector3(currentpos * ScreenWidth.x, 0, ScreenWidth.z);

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
        //move the blue tank to it's spawn location
        activeBlueTank.gameObject.transform.position = new Vector3(blueSpawnPosition.x + targetpos * ScreenWidth.x, 0, blueSpawnPosition.z);
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
        //move the red tank to it's spawn position
        activeRedTank.gameObject.transform.position = new Vector3(redSpawnPosition.x + targetpos * ScreenWidth.x,0,redSpawnPosition.z);
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

    /// <summary>
    /// advance the player between screens
    /// </summary>
    /// <param name="direction">what direction to move the screen in -1 for a red win, 1 for a blue win</param>
    private void Advance(int direction)
    {
        if(currentpos > winBy)
        {
            //call some winning function
        }

        targetpos += direction;
        screenMoving = true;

    }
    /// <summary>
    /// called by left bounds to advance the red tank
    /// </summary>
    public void RedAdvance()
    {
        //advance the red tank if you are able
        if(redAdvance)
        {
            Advance(-1);
            KillBlueTank();
            Destroy(currentLeftBounds.gameObject);
            Destroy(currentRightBounds.gameObject);

            //check if tanks are out of bounds
            Checkbounds(activeRedTank);
            Checkbounds(activeBlueTank);
        }
    }

    /// <summary>
    /// called by right bounds to advance the blue tank
    /// </summary>
    public void BluAdvance()
    {
        //advance the blue tank if you are able
        if(blueAdvance)
        {
            Advance(1);
            KillRedTank();
            Destroy(currentLeftBounds.gameObject);
            Destroy(currentRightBounds.gameObject);

            //check if tanks are out of bounds
            Checkbounds(activeRedTank);
            Checkbounds(activeBlueTank);
        }
    }

    private void RespawnBounds()
    {
        

        GameObject right = Instantiate(rightbounds);
        GameObject left = Instantiate(leftbounds);

        right.gameObject.transform.position = new Vector3(targetpos * ScreenWidth.x + .5f * ScreenWidth.x,0f,0f);
        left.gameObject.transform.position = new Vector3(targetpos * ScreenWidth.x - .5f * ScreenWidth.x,0f,0f);

        currentRightBounds = right;
        currentLeftBounds = left;


    }

    private void Checkbounds(GameObject tank)
    {
        if(tank == null)
        {
            return;
        }

        if (redAdvance)
        {
            if (tank.transform.position.x > (targetpos * ScreenWidth.x + .5f * ScreenWidth.x))
            {
                tank.transform.position = new Vector3((targetpos * ScreenWidth.x + .5f * ScreenWidth.x - .25f), tank.transform.position.y, tank.transform.position.z);
               
            }
        }
        else if (blueAdvance)
        {
            if (tank.transform.position.x < (targetpos * ScreenWidth.x - .5f * ScreenWidth.x))
            {
                tank.transform.position = new Vector3(((targetpos * ScreenWidth.x) - (.5f * ScreenWidth.x) + .25f), tank.transform.position.y, tank.transform.position.z);
                
            }
        }

        

        
    }


}
