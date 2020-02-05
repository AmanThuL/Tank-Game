using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Buttons
    public string menuSceneName, gameSceneName, controlsSceneName;

    public GameObject controlMenu, buttons;

    public GameObject sceneFader;

    public GameObject redArrow, blueArrow;

    private bool isControlOn;

    // Start is called before the first frame update
    void Start()
    {
        isControlOn = false;
        sceneFader = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (redArrow != null && blueArrow != null)
        {
            if (GameStats.blueAdvance)
                SetArrowActive(blueArrow, true);
            else SetArrowActive(blueArrow, false);

            if (GameStats.redAdvance)
                SetArrowActive(redArrow, true);
            else SetArrowActive(redArrow, false);
        }
    }

    // Button onclick
    public void StartGame()
    {
        sceneFader.SetActive(true);
        sceneFader.GetComponent<SceneFader>().FadeTo(gameSceneName);
    }

    //public void ToggleControlScreen()
    //{
    //    isControlOn = !isControlOn;
    //    controlMenu.SetActive(isControlOn);

    //    buttons.SetActive(!isControlOn);
    //}

    public void ToMainMenu()
    {
        //SceneManager.LoadScene(menuSceneName);
        sceneFader.SetActive(true);
        Debug.Log("Loading Main Menu!");
        sceneFader.GetComponent<SceneFader>().FadeTo(menuSceneName);
    }

    public void ToControlsScreen()
    {
        //SceneManager.LoadScene(controlsSceneName);
        sceneFader.SetActive(true);
        sceneFader.GetComponent<SceneFader>().FadeTo(controlsSceneName);
    }

    private void SetArrowActive(GameObject arrow, bool value)
    {
        arrow.GetComponent<Image>().enabled = value;
        if (!value) // inactive
        {
            arrow.GetComponent<BlinkingUI>().SetBlinkingTimes = 0;
        }
    }
}
