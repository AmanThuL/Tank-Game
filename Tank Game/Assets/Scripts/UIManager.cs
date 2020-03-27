using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    // Buttons
    public string menuSceneName, gameSceneName, controlsSceneName;

    public GameObject controlMenu, buttons;

    public GameObject sceneFader;

    public GameObject redArrow, blueArrow;

    public GameObject arrowUI;
    public GameObject lastSelected;
    private GameObject currentSelected;

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
        if (arrowUI != null)
        {
            currentSelected = EventSystem.current.GetComponent<EventSystem>().currentSelectedGameObject;

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

    public void RestartScene()
    {
        sceneFader.SetActive(true);
        sceneFader.GetComponent<SceneFader>().FadeTo(gameSceneName);
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
        
        Vector3 startPos = buttonTransform.position - new Vector3(buttonTransform.rect.width / 2f + 25f, 0, 0);
        Vector3 endPos = buttonTransform.position - new Vector3(buttonTransform.rect.width / 2f - 5f, 0, 0);
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
