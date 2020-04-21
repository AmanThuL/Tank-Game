using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    Flag = 0,
    Time = 1,
    Lives = 2
}

public class GameStats : Singleton<GameStats>
{
    #region Fields

    public bool blueAdvance = false, redAdvance = false;

    public bool isInputEnabled = false;

    public bool isGetFlagUIDisplayed = true;

    public int currScreenIndex = 0;

    public bool isPauseMenuEnabled = false;

    // Bullet Stats
    public int redMaxBullets = 5;
    public int blueMaxBullets = 5;
    public int redBullets = 5;
    public int blueBullets = 5;

    public Sprite player1Color;
    public Sprite player2Color;

    public int player1TankColor = 1;

    public int player2TankColor = 0;

    public Levels selectedLevel = 0;

    //Game Options Vars
    public float changeTime = 0;

    public bool limitedAmmo = true;
    public bool speedUp = true;
    public bool infAmmo = true;
    public bool doubleShot = true;
    public bool shield = true;

    public GameMode mode = GameMode.Lives;

    #region Lives
    //death occurs when reduced below zero, so if lives = 1 you must die 2 times before the game ends
    public int maxLives = 5;
    #endregion

    #region Time
    public float lengthInSeconds = 120f; 
	#endregion

	public bool powerUpSpawned;

    // Tank Color
    public Hashtable tankColor = new Hashtable {
         { 0,       new Color32( 228, 27, 27, 255 ) },      // Red
         { 1,      new Color32( 45, 68, 219, 255 ) },       // Blue
         { 2,    new Color32( 157, 14, 160, 255 ) },        // Violet
         { 3,     new Color32( 13, 199, 39, 255 ) },        // Green
         { 4,    new Color32( 196, 199, 13, 255 ) },        // Yellow
         { 5,    new Color32( 199, 106, 10, 255 ) },        // Orange
         { 6,      new Color32( 15, 177 , 169, 255 ) },     // Cyan
     };


    // UI
    public Powerup? currActivePowerup = null;

    #endregion
  

    //the number of screens from the middle screen you need to traverse to win.
    //overrides winby in the game manager script if it isn't negative 1
    //public static int NumScreens = 2;
    //not currently in use

    public int getBullets(char tankID)
    {
        if (tankID == 'R')
        { return redBullets; }
        if (tankID == 'B')
        { return blueBullets; }
        return -1;
    }

    public void incrementBullets(char tankID)
    {
        if (tankID == 'R')
        {
            redBullets++;
            redBullets = redBullets > redMaxBullets ? redMaxBullets : redBullets;
        }
        if (tankID == 'B')
        {
            blueBullets++;
            blueBullets = blueBullets > blueMaxBullets ? blueMaxBullets : blueBullets;
        }
    }

    public void decrementBullets(char tankID)
    {
        if (tankID == 'R')
        {
            redBullets--;
            redBullets = redBullets < 0 ? 0 : redBullets;
        }
        if (tankID == 'B')
        {
            blueBullets--;
            blueBullets = blueBullets < 0 ? 0 : blueBullets;
        }
    }

}
