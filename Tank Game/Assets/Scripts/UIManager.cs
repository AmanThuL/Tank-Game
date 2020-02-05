using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStats.blueAdvance)
            blueArrow.SetActive(true);
        else blueArrow.SetActive(false);

        if (GameStats.redAdvance)
            redArrow.SetActive(true);
        else redArrow.SetActive(false);
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
        sceneFader.GetComponent<SceneFader>().FadeTo(menuSceneName);
    }

    public void ToControlsScreen()
    {
        //SceneManager.LoadScene(controlsSceneName);
        sceneFader.SetActive(true);
        sceneFader.GetComponent<SceneFader>().FadeTo(controlsSceneName);
    }
}
