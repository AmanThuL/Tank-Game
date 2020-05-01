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
    JungleLevel,
    FireLevel,
    GrassLandsLevel
}

public enum Powerup
{
    DoubleShot,
    InfAmmo,
    Shield,
    SpeedUp,
    SpreadShot
}

public class UIManager : MonoBehaviour
{
    // Buttons
    public string menuSceneName, gameSceneName, controlsSceneName, gameOptionsSceneName, tankSelectionSceneName, levelSelectionSceneName, endScreenName;

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

    [Header("Level Selection UI")]
    public GameObject scrollbar;
    public GameObject scrollViewContent;
    private float distance;
    private float scrollPos = 0;
    private float[] pos;
    private int posIndex = 2;

    [Header("Tank UI")]
    public GameObject blueAmmoUI;
    public GameObject redAmmoUI;
    public GameObject tank_shell_UI;
    public GameObject tank_shell_UI_blank;
    [SerializeField] private List<GameObject> blueTankBulletsUIList;
    [SerializeField] private List<GameObject> redTankBulletsUIList;

    public Image bluePowerupUI, redPowerupUI;
    [SerializeField] private List<Sprite> powerupSprites;

    [Header("End Screen UI")]
    public Image winScreenUI;
    public Image mainMenuBtn, playAgainBtn;

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
            p1Controls.color = (Color32)GameStats.Instance.tankColor[GameStats.Instance.player1TankColor];
            p2Controls.color = (Color32)GameStats.Instance.tankColor[GameStats.Instance.player2TankColor];
        }

        // If the active scene is one of the levels
        if (System.Enum.IsDefined(typeof(Levels), SceneManager.GetActiveScene().name))
        {
            Color p1Color, p2Color;
            p1Color = (Color32)GameStats.Instance.tankColor[GameStats.Instance.player1TankColor];
            p2Color = (Color32)GameStats.Instance.tankColor[GameStats.Instance.player2TankColor];

            // Powerup UI starts disabled
            bluePowerupUI.enabled = false;
            redPowerupUI.enabled = false;

            p1GoUI.color = p1Color;
            p2GoUI.color = p2Color;

            blueAmmoUI.GetComponent<Image>().color = new Color(p1Color.r, p1Color.g, p1Color.b, 0.8f);
            redAmmoUI.GetComponent<Image>().color = new Color(p2Color.r, p2Color.g, p2Color.b, 0.8f);

            // Initiate the ammo UI
            for (int i = 0; i < GameStats.Instance.blueBullets; ++i)
            {
                GameObject bulletUI1 = Instantiate(tank_shell_UI, Vector3.zero, Quaternion.identity);
                RectTransform rect1 = bulletUI1.GetComponent<RectTransform>();
                rect1.Rotate(new Vector3(0, 0, 90f));
                bulletUI1.transform.SetParent(blueAmmoUI.transform);
                rect1.localScale = new Vector3(1f, 1f, 1f);
                blueTankBulletsUIList.Add(bulletUI1);

                GameObject bulletUI2 = Instantiate(tank_shell_UI, Vector3.zero, Quaternion.identity);
                RectTransform rect2 = bulletUI2.GetComponent<RectTransform>();
                rect2.Rotate(new Vector3(0, 0, 90f));
                bulletUI2.transform.SetParent(redAmmoUI.transform);
                rect2.localScale = new Vector3(1f, 1f, 1f);
                redTankBulletsUIList.Add(bulletUI2);
            }
        }

        // setup arrow UI
        if (arrowUI)
        {
            PointToSelectedButton(EventSystem.current.GetComponent<EventSystem>().firstSelectedGameObject);
        }

        if (SceneManager.GetActiveScene().name == "LevelSelection")
        {
            pos = new float[scrollViewContent.transform.childCount];
            distance = 1f / (pos.Length - 1f);
            for (int i = 0; i < pos.Length; i++)
            {
                pos[i] = distance * i;
            }
            posIndex = 0;
        }

        // If current scene is endscreen
        if (SceneManager.GetActiveScene().name == "EndScreen")
        {
            Color winnerColor;
            switch (GameStats.Instance.winPlayer)
            {
                case 1:
                    winnerColor = (Color32)GameStats.Instance.tankColor[GameStats.Instance.player1TankColor];
                    break;
                case 2:
                    winnerColor = (Color32)GameStats.Instance.tankColor[GameStats.Instance.player2TankColor];
                    break;
                default:
                    winnerColor = (Color32)GameStats.Instance.tankColor[GameStats.Instance.player1TankColor];
                    break;
            }
            winScreenUI.color = mainMenuBtn.color = playAgainBtn.color = winnerColor;
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

        if (SceneManager.GetActiveScene().name.Equals("LevelSelection"))
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (GameStats.Instance.currentSelectedGameObject().name != "BackBtn")
                {
                    NextLevelCard();
                    EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(scrollViewContent.transform.GetChild(posIndex).gameObject);
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (GameStats.Instance.currentSelectedGameObject().name != "BackBtn")
                {
                    PrevLevelCard();
                    EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(scrollViewContent.transform.GetChild(posIndex).gameObject);
                }
            }
            else if (Input.GetMouseButton(0))
            {
                scrollPos = scrollbar.GetComponent<Scrollbar>().value;
            }
            else
            {
                for (int i = 0; i < pos.Length; i++)
                {
                    if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
                    {
                        scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                        posIndex = i;
                    }
                }
            }

            for (int i = 0; i < pos.Length; i++)
            {
                if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
                {
                    scrollViewContent.transform.GetChild(i).localScale =
                        Vector2.Lerp(scrollViewContent.transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                    for (int j = 0; j < pos.Length; j++)
                    {
                        if (j != i)
                        {
                            scrollViewContent.transform.GetChild(j).localScale =
                                Vector2.Lerp(scrollViewContent.transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                        }
                    }
                }
            }
        }


        // When a different button is selected, enable its border and start flickering
        GameObject tempSelectedGO = EventSystem.current.GetComponent<EventSystem>().currentSelectedGameObject;
        if (tempSelectedGO && tempSelectedGO.CompareTag("NavigatableButton"))
        {
            currentSelected = GameStats.Instance.currentSelectedGameObject();
        }
        else if (SceneManager.GetActiveScene().name.Equals("TankSelection"))
        {
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(currentSelected);
        }

        // Select on change
        if (currentSelected != null && currentSelected != lastSelected)
        {
            ToggleSelectedButtonBorder(lastSelected, false);
            lastSelected = currentSelected;
            ToggleSelectedButtonBorder(currentSelected, true);
        }
        else if (currentSelected == null)
        {
            // set border invisible
            ToggleSelectedButtonBorder(currentSelected, false);
        }


        if (redArrow != null && blueArrow != null)
        {
            if (GameStats.Instance.blueAdvance)
                SetArrowActive(blueArrow, true);
            else SetArrowActive(blueArrow, false);

            if (GameStats.Instance.redAdvance)
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
        GameStats.Instance.isPauseMenuEnabled = false;
    }

    public void ToGameOptions()
    {
        sceneFader.SetActive(true);
        sceneFader.GetComponent<SceneFader>().FadeTo(gameOptionsSceneName);
        GameStats.Instance.isPauseMenuEnabled = false;
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

        Debug.Log(GameStats.Instance.selectedLevel.ToString());
        sceneFader.GetComponent<SceneFader>().FadeTo(GameStats.Instance.selectedLevel.ToString());
    }

    public void ToEndScreen()
    {
        sceneFader.SetActive(true);
        sceneFader.GetComponent<SceneFader>().FadeTo(endScreenName);
    }

    public void Desert()
    {
        GameStats.Instance.selectedLevel = Levels.DesertLevel;
        ToSelectedLevel();
    }

    public void Ice()
    {
        GameStats.Instance.selectedLevel = Levels.IceLevel;
        ToSelectedLevel();
    }

    public void Jungle()
    {
        GameStats.Instance.selectedLevel = Levels.JungleLevel;
        ToSelectedLevel();
    }

    public void Fire()
    {
        GameStats.Instance.selectedLevel = Levels.FireLevel;
        ToSelectedLevel();
    }

    public void Grass()
    {
        GameStats.Instance.selectedLevel = Levels.GrassLandsLevel;
        ToSelectedLevel();
    }

    public void RestartGameLevel()
    {
        sceneFader.SetActive(true);
        sceneFader.GetComponent<SceneFader>().FadeTo(GameStats.Instance.selectedLevel.ToString());
    }

    public void UnpauseGame()
    {
        GameStats.Instance.isInputEnabled = true;
        GameStats.Instance.isPauseMenuEnabled = false;

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

    private void ToggleSelectedButtonBorder(GameObject button, bool isEnabled)
    {
        if (!button) return;
        if (button.transform.childCount == 0) return;
        GameObject border = button.transform.GetChild(0).gameObject;
        border.GetComponent<Image>().enabled = isEnabled;
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

    public void UpdateAmmoUI(char color)
    {
        int currentBulletCount;
        int maxBulletCount;
        switch (color)
        {
            case 'B':
                currentBulletCount = GameStats.Instance.blueBullets;
                maxBulletCount = GameStats.Instance.blueMaxBullets;
                for (int i = 0; i < maxBulletCount; i++)
                {
                    blueTankBulletsUIList[i].GetComponent<Image>().enabled = i < currentBulletCount ? true : false;
                }
                break;
            case 'R':
                currentBulletCount = GameStats.Instance.redBullets;
                maxBulletCount = GameStats.Instance.redMaxBullets;
                for (int i = 0; i < maxBulletCount; i++)
                {
                    redTankBulletsUIList[i].GetComponent<Image>().enabled = i < currentBulletCount ? true : false;
                }
                break;
        }
    }

    public void ActivatePowerupUI(float delay, bool isBlue)
    {
        StartCoroutine(PowerupUITimer(delay, isBlue));
    }

    private IEnumerator PowerupUITimer(float delay, bool isBlue)
    {
        Image powerupUI = isBlue ? bluePowerupUI : redPowerupUI;
        powerupUI.enabled = true;
        powerupUI.sprite = powerupSprites[(int)(GameStats.Instance.currActivePowerup)];
        powerupUI.fillAmount = 1;

        float time = delay;
        while (time > 0.0f)
        {
            time -= Time.deltaTime;
            powerupUI.fillAmount = time / delay;
            yield return new WaitForEndOfFrame();
        }

        powerupUI.enabled = false;
    }

    private void NextLevelCard()
    {
        posIndex++;
        posIndex = Mathf.Clamp(posIndex, 0, pos.Length - 1);
        scrollPos = pos[posIndex];
    }

    private void PrevLevelCard()
    {
        posIndex--;
        posIndex = Mathf.Clamp(posIndex, 0, pos.Length - 1);
        scrollPos = pos[posIndex];
    }

    public void ToggleLimitedAmmo() { GameStats.Instance.limitedAmmo = !GameStats.Instance.limitedAmmo; }
    public void ToggleSpeedUp() { GameStats.Instance.speedUp = !GameStats.Instance.speedUp; }
    public void ToggleInfiniteAmmo() { GameStats.Instance.infAmmo = !GameStats.Instance.infAmmo; }
    public void ToggleDoubleShot() { GameStats.Instance.doubleShot = !GameStats.Instance.doubleShot; }
    public void ToggleShield() { GameStats.Instance.shield = !GameStats.Instance.shield; }
}
