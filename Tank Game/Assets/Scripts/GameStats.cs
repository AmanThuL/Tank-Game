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

    public static int redBullets = 3;

    public static int blueBullets = 3;

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
            if (redBullets > 3) redBullets = 3;
        }
        if (tankID == 'B')
        { 
            blueBullets++;
            if (blueBullets > 3) blueBullets = 3;
        }

        
    }

    public static void decrementBullets(char tankID)
    {
        if (tankID == 'R')
        {
            redBullets--;
            if (redBullets <0) redBullets = 0;
        }
        if (tankID == 'B')
        {
            blueBullets--;
            if (blueBullets < 0 ) blueBullets = 0;
        }


    }
}
