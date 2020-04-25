using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeSettings : MonoBehaviour
{
    //Game mode buttons
    public GameObject flagButton;
    public GameObject stockButton;
    public GameObject timeButton;

    //Game mode images
    public Sprite flagOn;
    public Sprite flagOff;
    public Sprite stockOn;
    public Sprite stockOff;
    public Sprite timeOn;
    public Sprite timeOff;

    void Start()
    {
        switch (GameStats.Instance.mode)
        {
            case GameMode.Flag:

                flagButton.GetComponent<Image>().sprite = flagOn;
                stockButton.GetComponent<Image>().sprite = stockOff;
                timeButton.GetComponent<Image>().sprite = timeOff;

                break;
            case GameMode.Time:

                flagButton.GetComponent<Image>().sprite = flagOff;
                stockButton.GetComponent<Image>().sprite = stockOn;
                timeButton.GetComponent<Image>().sprite = timeOn;

                break;
            case GameMode.Lives:

                flagButton.GetComponent<Image>().sprite = flagOff;
                stockButton.GetComponent<Image>().sprite = stockOn;
                timeButton.GetComponent<Image>().sprite = timeOff;

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
            flagButton.GetComponent<Image>().sprite = flagOn;
        }
        else
        {
            GameStats.Instance.mode = GameMode.Flag;
            flagButton.GetComponent<Image>().sprite = flagOn;
            stockButton.GetComponent<Image>().sprite = stockOff;
            timeButton.GetComponent<Image>().sprite = timeOff;
        }
    }

    public void TimeModeOn()
    {
        if (GameStats.Instance.mode == GameMode.Time)
        {
            stockButton.GetComponent<Image>().sprite = stockOn;
        }
        else
        {
            GameStats.Instance.mode = GameMode.Time;
            flagButton.GetComponent<Image>().sprite = flagOff;
            stockButton.GetComponent<Image>().sprite = stockOff;
            timeButton.GetComponent<Image>().sprite = timeOn;
        }
    }

    public void StockModeOn()
    {
        if (GameStats.Instance.mode == GameMode.Lives)
        {
            stockButton.GetComponent<Image>().sprite = stockOn;
        }
        else
        {
            GameStats.Instance.mode = GameMode.Lives;
            flagButton.GetComponent<Image>().sprite = flagOff;
            stockButton.GetComponent<Image>().sprite = stockOn;
            timeButton.GetComponent<Image>().sprite = timeOff;
        }
    }

}
