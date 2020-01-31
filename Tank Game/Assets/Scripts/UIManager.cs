using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Buttons
    public string menuSceneName, gameSceneName;

    public GameObject controlMenu, buttons;

    private bool isControlOn;

    // Start is called before the first frame update
    void Start()
    {
        isControlOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Button onclick
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void ToggleControlScreen()
    {
        isControlOn = !isControlOn;
        controlMenu.SetActive(isControlOn);

        buttons.SetActive(!isControlOn);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
