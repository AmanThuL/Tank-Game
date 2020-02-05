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
    
    [Header("Respawn Information")]
    [SerializeField] [Range(0, 5)] float SpawnDelay;
    [SerializeField] [Range(0,2)] float spawnDelayIncrement;
    float currentRedSpawnDelay;
    float currentBlueSpawnDelay;
    float spawnTimerBlue = 0f;
    float spawnTimerRed = 0f;
    bool blueDead, redDead;
    bool redInvincible = false, BlueInvincible = false;
    [SerializeField] [Range(0, 5)] float invinciblityTime;
    float redInvulnTimer = 0, bluInvulnTimer=0;
    Color normalColor = new Color(255, 255, 255, 255);
    [SerializeField] Color InvulnColor = new Color(255, 255, 255, 100);

    [Header("Screen Transition")]
    [SerializeField] GameObject flag; //holds the item that will be captured
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

    public GameObject blueTankWinsUI, redTankWinsUI;
    public GameObject getFlagCanvasUI;

    [Header("Tanks Properties")]
    [SerializeField] [Range(0, 10)] private float accelRate;
    [SerializeField] [Range(0, 1)] private float deceleration;
    [SerializeField] [Range(0, 20)] private float maxSpeed;
    [SerializeField] [Range(0, 360)] private float turnSpeed;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject destroyedDecal;


    // Start is called before the first frame update
    void Start()
    {
        GameStats.isInputEnabled = true;
        GameStats.blueAdvance = false;
        GameStats.redAdvance = false;
        GameStats.isGetFlagUIDisplayed = true;
        getFlagCanvasUI.SetActive(GameStats.isGetFlagUIDisplayed);

        Time.timeScale = 1;
        resetSpawnDelay();
        SpawnFlag(new Vector3(0,-2,0));
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
        UpdateInvulnTimer(Time.deltaTime);
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
            if (spawnTimerBlue > currentBlueSpawnDelay)
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
            if (spawnTimerRed > currentRedSpawnDelay)
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

    void UpdateInvulnTimer(float dt)
    {
        if(redInvincible)
        {
            redInvulnTimer += dt;
            if(redInvulnTimer > invinciblityTime)
            {
                redInvincible = false;
                redInvulnTimer = 0;
                SetTankAlpha(normalColor, activeRedTank);
            }
        }

        if(BlueInvincible)
        {
            //increment timer
            bluInvulnTimer += dt;
            //check if invincibility is over
            if(bluInvulnTimer > invinciblityTime)
            {
                BlueInvincible = false;
                bluInvulnTimer = 0;
                SetTankAlpha(normalColor, activeBlueTank);
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
        BlueInvincible = true;
        SetTankAlpha(InvulnColor,activeBlueTank);

        AssignTankProperties(activeBlueTank);
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
        redInvincible = true;
        SetTankAlpha(InvulnColor, activeRedTank);

        AssignTankProperties(activeRedTank);
        //move the red tank to it's spawn position
        activeRedTank.gameObject.transform.position = new Vector3(redSpawnPosition.x + targetpos * ScreenWidth.x,0,redSpawnPosition.z);
    }

    /// <summary>
    /// kill the blue tank
    /// </summary>
    public void KillBlueTank()
    {
        if(BlueInvincible)
        {
            return;
        }

        //begin the timer for the blue tank
        blueDead = true;
        spawnTimerBlue = 0f;
        currentBlueSpawnDelay += spawnDelayIncrement;
        //set which tank can advance to win
        //redAdvance = true;

        Vector3 temp = activeBlueTank.transform.position;

        //destroy the blue tank
        GameObject.Destroy(activeBlueTank); 
        Instantiate(explosion, temp, Quaternion.identity);
        Instantiate(destroyedDecal, new Vector3(temp.x, temp.y, 10), Quaternion.identity);

        if (GameStats.blueAdvance)
        {
            GameStats.blueAdvance = false;
            SpawnFlag(temp);
        }
    }

    /// <summary>
    /// Kill the red tank
    /// </summary>
    public void KillRedTank()
    {
        if(redInvincible)
        {
            return;
        }

        //begin the timer for the red tank
        redDead = true;
        spawnTimerRed = 0f;
        currentRedSpawnDelay += spawnDelayIncrement;
        //set which tank can advance to win
        //blueAdvance = true;

        Vector3 temp = activeRedTank.transform.position;
        

        //destroy the red tank
        GameObject.Destroy(activeRedTank);
        Instantiate(explosion, temp, Quaternion.identity);
        Instantiate(destroyedDecal, new Vector3(temp.x, temp.y, 10), Quaternion.identity);

        if (GameStats.redAdvance)
        {
            GameStats.redAdvance = false;
            SpawnFlag(temp);
        }
    }

    /// <summary>
    /// advance the player between screens
    /// </summary>
    /// <param name="direction">what direction to move the screen in -1 for a red win, 1 for a blue win</param>
    private void Advance(int direction)
    {
        GameStats.isGetFlagUIDisplayed = false;
        getFlagCanvasUI.SetActive(GameStats.isGetFlagUIDisplayed);

        targetpos += direction;
        
        if (Mathf.Abs(targetpos) > winBy)
        {
            GameStats.isInputEnabled = false;
            //call some winning function
            switch (direction)
            {
                case -1:
                    redTankWinsUI.SetActive(true);
                    break;
                case 1:
                    blueTankWinsUI.SetActive(true);
                    break;
            }

            return;
        }

        screenMoving = true;

        resetSpawnDelay();

    }
    /// <summary>
    /// called by left bounds to advance the red tank
    /// </summary>
    public void RedAdvance()
    {
        //advance the red tank if you are able
        if(GameStats.redAdvance)
        {
            Advance(-1);
            KillBlueTank();
            Destroy(currentLeftBounds.gameObject);
            Destroy(currentRightBounds.gameObject);

        }
    }

    /// <summary>
    /// called by right bounds to advance the blue tank
    /// </summary>
    public void BluAdvance()
    {
        //advance the blue tank if you are able
        if(GameStats.blueAdvance)
        {
            Advance(1);
            KillRedTank();
            Destroy(currentLeftBounds.gameObject);
            Destroy(currentRightBounds.gameObject);

            
        }
    }

    /// <summary>
    /// Respawns the bounds around the screen
    /// </summary>
    private void RespawnBounds()
    {
        
        //create the left and right bounds
        GameObject right = Instantiate(rightbounds);
        GameObject left = Instantiate(leftbounds);
        //place the left and right bounds
        right.gameObject.transform.position = new Vector3(targetpos * ScreenWidth.x + .5f * ScreenWidth.x,0f,0f);
        left.gameObject.transform.position = new Vector3(targetpos * ScreenWidth.x - .5f * ScreenWidth.x,0f,0f);
        //keep track of the left and right bounds
        currentRightBounds = right;
        currentLeftBounds = left;

        //check if tanks are out of bounds
        Checkbounds(activeRedTank);
        Checkbounds(activeBlueTank);
    }


    /// <summary>
    /// Checks to see if a tank is outside the bounds
    /// </summary>
    /// <param name="tank"> the game object to check outside the bounds, if tank is null the check is not preformed as the tank is presumed dead</param>
    private void Checkbounds(GameObject tank)
    {
        if(tank == null)
        {
            return;
        }

        if (GameStats.redAdvance)
        {
            if (tank.transform.position.x > (targetpos * ScreenWidth.x + .5f * ScreenWidth.x))
            {
                tank.transform.position = new Vector3((targetpos * ScreenWidth.x + .5f * ScreenWidth.x - .25f), tank.transform.position.y, tank.transform.position.z);
               
            }
        }
        else if (GameStats.blueAdvance)
        {
            if (tank.transform.position.x < (targetpos * ScreenWidth.x - .5f * ScreenWidth.x))
            {
                tank.transform.position = new Vector3(((targetpos * ScreenWidth.x) - (.5f * ScreenWidth.x) + .25f), tank.transform.position.y, tank.transform.position.z);
                
            }
        }

        

        
    }

    /// <summary>
    /// changes the properties on the active tank
    /// </summary>
    /// <param name="tank"></param>
    private void AssignTankProperties(GameObject tank)
    {
        tank.GetComponent<BlueTankControls>().AccelRate = accelRate;
        tank.GetComponent<BlueTankControls>().Deceleration = deceleration;
        tank.GetComponent<BlueTankControls>().MaxSpeed = maxSpeed;
        tank.GetComponent<BlueTankControls>().TurnSpeed = turnSpeed;
    }

    /// <summary>
    /// resets the spawn delay, called when the scene is changed to set the spawn delay back to the initial spawn delay
    /// </summary>
    private void resetSpawnDelay()
    {
        currentBlueSpawnDelay = SpawnDelay;
        currentRedSpawnDelay = SpawnDelay;
    }

    /// <summary>
    /// sets the tanks color to the given color
    /// </summary>
    /// <param name="alpha"> the color to change the tank</param>
    /// <param name="tank">the tank to modify</param>
    private void SetTankAlpha(Color alpha, GameObject tank)
    {
        tank.GetComponent<SpriteRenderer>().color = alpha;
    }

    public void RedCaptureFlag()
    {
        GameStats.redAdvance = true;
    }

    public void BlueCaptureFlag()
    {
        GameStats.blueAdvance = true;
    }

    /// <summary>
    /// spawns a flag at the given location
    /// </summary>
    /// <param name="pos">the location to spawn the flag at</param>
    void SpawnFlag(Vector3 pos)
    {
        GameObject temp = Instantiate(flag);
        temp.transform.position = pos;
    }
}
