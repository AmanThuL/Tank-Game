using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeSettings : MonoBehaviour
{

    public Toggle flagToggle;
    public Toggle stockToggle;
    public Toggle timeToggle;

    void Start()
    {
        switch (GameStats.Instance.mode)
        {
            case GameMode.Flag:

                flagToggle.isOn = true;
                stockToggle.isOn = false;
                timeToggle.isOn = false;

                break;
            case GameMode.Time:

                flagToggle.isOn = false;
                stockToggle.isOn = false;
                timeToggle.isOn = true;

                break;
            case GameMode.Lives:

                flagToggle.isOn = false;
                stockToggle.isOn = true;
                timeToggle.isOn = false;

                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FlagModeOn()
    {
        if (GameStats.Instance.mode == GameMode.Flag)
        {
            flagToggle.isOn = true;
        }
        else
        {
            GameStats.Instance.mode = GameMode.Flag;
            flagToggle.isOn = true;
            stockToggle.isOn = false;
            timeToggle.isOn = false;
        }
    }

    public void TimeModeOn()
    {
        if (GameStats.Instance.mode == GameMode.Time)
        {
            timeToggle.isOn = true;
        }
        else
        {
            GameStats.Instance.mode = GameMode.Time;
            flagToggle.isOn = false;
            stockToggle.isOn = false;
            timeToggle.isOn = true;
        }
    }

    public void StockModeOn()
    {
        if (GameStats.Instance.mode == GameMode.Lives)
        {
            stockToggle.isOn = true;
        }
        else
        {
            GameStats.Instance.mode = GameMode.Lives;
            flagToggle.isOn = false;
            stockToggle.isOn = true;
            timeToggle.isOn = false;
        }
    }

}
