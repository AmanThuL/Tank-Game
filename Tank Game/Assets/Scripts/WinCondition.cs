using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    private float totalCamHeight;               // the height of the camera
    private float totalCamWidth;                // the width of the camera
    public GameObject blueTank;
    public GameObject WinScreenBackground;
    public GameObject Winner;

    // Start is called before the first frame update
    void Start()
    {
        Camera camObject = Camera.main;
        totalCamHeight = camObject.orthographicSize * 2f;       // Orthographic size is half the height of the camera
        totalCamWidth = totalCamHeight * camObject.aspect;      // Get the width of the camera
        WinScreenBackground.SetActive(false);
        Winner.GetComponent<Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        playerWins();
    }

    public void playerWins()
    {
        if (blueTank.transform.position.x > totalCamWidth / 2f)
        {
            WinScreenBackground.SetActive(true);
            Winner.GetComponent<Text>().text = "Blue Tank Wins";
            Winner.GetComponent<Text>().color = new Color(0,0,255,1);
        }

        if (blueTank.transform.position.x > totalCamWidth / 2f) //change tank to red tank
        {
            WinScreenBackground.SetActive(true);
            Winner.GetComponent<Text>().text = "Red Tank Wins";
            Winner.GetComponent<Text>().color = new Color(255, 0, 0, 1);
        }
    }


}