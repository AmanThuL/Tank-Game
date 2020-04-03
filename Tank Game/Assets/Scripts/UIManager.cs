using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum Levels
{
    DesertLevel,
    IceLevel,
    JungleLevel
}

public class UIManager : MonoBehaviour
{
    // Buttons
    public string menuSceneName, gameSceneName, controlsSceneName, tankSelectionSceneName, levelSelectionSceneName;

    public GameObject controlMenu, buttons;

    public GameObject sceneFader;

    public GameObject redArrow, blueArrow;

    public GameObject arrowUI;
    public GameObject lastSelected;
    private GameObject currentSelected;

    public GameObject pauseObj;

    public Image p1Controls;
    public Image p2Controls;

    public Image p1GoUI;
    public Image p2GoUI;

    private bool isControlOn;

    private float scaler;

    private void Awake()
    {
        Globals.resolution = new Vector2(Screen.width, Screen.height);
    }

    // Start is called before the first frame update
    void Start()
    {
        isControlOn = false;
        sceneFader = transform.GetChild(0).gameObject;

        // Calculate the scaler based on current resolution
        scaler = Globals.GetScale(Screen.height, Screen.width, new Vector2(1920, 1080), 0.5f);

        if (SceneManager.GetActiveScene().name == "Controls")
        {
            p1Controls.color = (Color32)GameStats.tankColor[GameStats.player1TankColor];
            p2Controls.color = (Color32)GameStats.tankColor[GameStats.player2TankColor];
        }

        if (System.Enum.IsDefined(typeof(Levels), SceneManager.GetActiveScene().name))
        {
            p1GoUI.color = (Color32)GameStats.tankColor[GameStats.player1TankColor];
            p2GoUI.color = (Color32)GameStats.tankColor[GameStats.player2TankColor];
        }

        // setup arrow UI
        if (arrowUI)
        {
            PointToSelectedButton(EventSystem.current.GetComponent<EventSystem>().firstSelectedGameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Globals.resolution.x != Screen.width || Globals.resolution.y != Screen.height)
        {
            Globals.resolution.x = Screen.width;
            Globals.resolution.y = Screen.height;

            // do stuff
            // update scaler
            scaler = Globals.GetScale(Screen.height, Screen.width, new Vector2(1920, 1080), 1f);

        }
#endif //UNITY_EDITOR

        if (arrowUI != null)
        {
            GameObject tempSelectedGO = EventSystem.current.GetComponent<EventSystem>().currentSelectedGameObject;

            if (tempSelectedGO && tempSelectedGO.CompareTag("NavigatableButton"))
            {
                currentSelected = EventSystem.current.GetComponent<EventSystem>().currentSelectedGameObject;
            }
            else if (SceneManager.GetActiveScene().name.Equals("TankSelection"))
            {
                EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(currentSelected);
            }

            // Select on change
            if (currentSelected != null && currentSelected != lastSelected)
            {
                lastSelected = currentSelected;
                PointToSelectedButton(currentSelected);
            }
            else if (currentSelected == null)
            {
                // set self invisible
                arrowUI.SetActive(false);
            }
        }

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
        //Debug.Log("Loading Main Menu!");
        sceneFader.GetComponent<SceneFader>().FadeTo(menuSceneName);
        GameStats.isPauseMenuEnabled = false;
    }

    public void ToControlsScreen()
    {
        //SceneManager.LoadScene(controlsSceneName);
        sceneFader.SetActive(true);
        sceneFader.GetComponent<SceneFader>().FadeTo(controlsSceneName);
    }

    public void ToSelectionScreen()
    {
        sceneFader.SetActive(true);
        sceneFader.GetComponent<SceneFader>().FadeTo(tankSelectionSceneName);
    }

    public void ToLevelSelectionScreen()
    {
        sceneFader.SetActive(true);
        sceneFader.GetComponent<SceneFader>().FadeTo(levelSelectionSceneName);
    }

    private void ToSelectedLevel()
    {
        sceneFader.SetActive(true);

        Debug.Log(GameStats.selectedLevel.ToString());
        sceneFader.GetComponent<SceneFader>().FadeTo(GameStats.selectedLevel.ToString());
    }

    public void Desert()
    {
        GameStats.selectedLevel = Levels.DesertLevel;
        ToSelectedLevel();
    }

    public void Ice()
    {
        GameStats.selectedLevel = Levels.IceLevel;
        ToSelectedLevel();
    }

    public void Jungle()
    {
        GameStats.selectedLevel = Levels.JungleLevel;
        ToSelectedLevel();
    }

    public void RestartGameLevel()
    {
        sceneFader.SetActive(true);
        sceneFader.GetComponent<SceneFader>().FadeTo(GameStats.selectedLevel.ToString());
    }

    public void UnpauseGame()
    {
        GameStats.isInputEnabled = true;
        GameStats.isPauseMenuEnabled = false;

        arrowUI = null;
        //Destroy pause screen
        pauseObj.SetActive(false);
    }

    private void SetArrowActive(GameObject arrow, bool value)
    {
        arrow.GetComponent<Image>().enabled = value;
        if (!value) // inactive
        {
            arrow.GetComponent<BlinkingUI>().SetBlinkingTimes = 0;
        }
    }

    private void PointToSelectedButton(GameObject button)
    {
        arrowUI.SetActive(true);
        RectTransform buttonTransform = button.GetComponent<RectTransform>();

        // Invert color
        //arrowUI.GetComponent<Image>().color = Globals.Invert(arrowUI.GetComponent<Image>().color);

        Vector3 startPos = buttonTransform.position - new Vector3(buttonTransform.rect.width / 2f * 1.15f * scaler, 0, 0);
        Vector3 endPos = buttonTransform.position - new Vector3(buttonTransform.rect.width / 2f * 1.1f * scaler, 0, 0);
        StopAllCoroutines();
        StartCoroutine(MoveArrowHorizontally(arrowUI.GetComponent<RectTransform>(), startPos, endPos));
    }

    private IEnumerator MoveArrow(RectTransform arrowTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            arrowTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }

    private IEnumerator MoveArrowHorizontally(RectTransform arrowTransform, Vector3 startPos, Vector3 endPos)
    {
        while (true)
        {
            yield return StartCoroutine(MoveArrow(arrowTransform, startPos, endPos, 1.0f));
            yield return StartCoroutine(MoveArrow(arrowTransform, endPos, startPos, 1.0f));
        }
    }
}
