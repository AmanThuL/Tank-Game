using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStats
{
    public static bool blueAdvance = false, redAdvance = false;

    public static bool isInputEnabled = false;

    public static bool isGetFlagUIDisplayed = true;

    public static int currScreenIndex = 0;

    public static bool isPauseMenuEnabled = false;

    public static int redBullets = 5;

    public static int blueBullets = 5;

    public static Sprite player1Color;

    public static Sprite player2Color;

    public static int player1TankColor = 1;

    public static int player2TankColor = 0;

    // Tank Color
    public static Hashtable tankColor = new Hashtable {
         { 0,       new Color32( 228, 27, 27, 255 ) },      // Red
         { 1,      new Color32( 45, 68, 219, 255 ) },       // Blue
         { 2,    new Color32( 157, 14, 160, 255 ) },        // Violet
         { 3,     new Color32( 13, 199, 39, 255 ) },        // Green
         { 4,    new Color32( 196, 199, 13, 255 ) },        // Yellow
         { 5,    new Color32( 199, 106, 10, 255 ) },        // Orange
         { 6,      new Color32( 15, 177 , 169, 255 ) },     // Cyan
     };

    //the number of screens from the middle screen you need to traverse to win.
    //overrides winby in the game manager script if it isn't negative 1
    //public static int NumScreens = 2;
    //not currently in use

    public static int getBullets(char tankID)
    {
        if (tankID == 'R')
        { return redBullets; }
        if (tankID == 'B')
        { return blueBullets; }
        return -1;
    }

    public static void incrementBullets(char tankID)
    {
        if (tankID == 'R')
        {
            redBullets++;
            if (redBullets > 5) redBullets = 5;
        }
        if (tankID == 'B')
        {
            blueBullets++;
            if (blueBullets > 5) blueBullets = 5;
        }


    }

    public static void decrementBullets(char tankID)
    {
        if (tankID == 'R')
        {
            redBullets--;
            if (redBullets < 0) redBullets = 0;
        }
        if (tankID == 'B')
        {
            blueBullets--;
            if (blueBullets < 0) blueBullets = 0;
        }
    }
}
