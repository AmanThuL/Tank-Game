using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{

    public GameObject leftTank;
    public GameObject rightTank;

    public Sprite redTank;
    public Sprite blueTank;
    public Sprite violetTank;
    public Sprite greenTank;
    public Sprite yellowTank;
    public Sprite orangeTank;
    public Sprite cyanTank;

    Sprite[] sprites;

    int rightIndex;
    int leftIndex;

    public GameObject leftL, leftR, rightL, rightR;
    private Button leftL_btn, leftR_btn, rightL_btn, rightR_btn;

    void Start()
    {
        leftIndex = GameStats.Instance.player1TankColor;
        rightIndex = GameStats.Instance.player2TankColor;

        sprites = new Sprite[7];
        sprites[0] = redTank;
        sprites[1] = blueTank;
        sprites[2] = violetTank;
        sprites[3] = greenTank;
        sprites[4] = yellowTank;
        sprites[5] = orangeTank;
        sprites[6] = cyanTank;

        leftL_btn = leftL.GetComponent<Button>();
        leftR_btn = leftR.GetComponent<Button>();
        rightL_btn = rightL.GetComponent<Button>();
        rightR_btn = rightR.GetComponent<Button>();
    }

    void Update()
    {
        ChangeSprites();
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Button interaction with keypress

            ButtonFadesToColor(leftL, leftL_btn.colors.pressedColor);

            LeftTankLeft();
            GameStats.Instance.player1Color = sprites[leftIndex];
            GameStats.Instance.player1TankColor = leftIndex;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            ButtonFadesToColor(leftL, leftL_btn.colors.normalColor);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            // Button interaction with keypress
            ButtonFadesToColor(leftR, leftR_btn.colors.pressedColor);

            LeftTankRight();
            GameStats.Instance.player1Color = sprites[leftIndex];
            GameStats.Instance.player1TankColor = leftIndex;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            ButtonFadesToColor(leftR, leftR_btn.colors.normalColor);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Button interaction with keypress
            ButtonFadesToColor(rightL, rightL_btn.colors.pressedColor);

            RightTankLeft();
            GameStats.Instance.player2Color = sprites[rightIndex];
            GameStats.Instance.player2TankColor = rightIndex;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            ButtonFadesToColor(rightL, rightL_btn.colors.normalColor);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Button interaction with keypress
            ButtonFadesToColor(rightR, rightR_btn.colors.pressedColor);

            RightTankRight();
            GameStats.Instance.player2Color = sprites[rightIndex];
            GameStats.Instance.player2TankColor = rightIndex;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            ButtonFadesToColor(rightR, rightR_btn.colors.normalColor);
        }
    }

    void ChangeSprites()
    {
        leftTank.GetComponent<Image>().sprite = sprites[leftIndex];
        rightTank.GetComponent<Image>().sprite = sprites[rightIndex];
    }

    public void LeftTankLeft()
    {
        if (leftIndex == 0)
        {
            leftIndex = 6;
        }
        else
        {
            leftIndex--;
        }

        if (leftIndex == rightIndex)
        {
            LeftTankLeft();
        }
    }

    public void LeftTankRight()
    {
        if (leftIndex == 6)
        {
            leftIndex = 0;
        }
        else
        {
            leftIndex++;
        }

        if (leftIndex == rightIndex)
        {
            LeftTankRight();
        }
    }

    public void RightTankLeft()
    {
        if (rightIndex == 0)
        {
            rightIndex = 6;
        }
        else
        {
            rightIndex--;
        }

        if (leftIndex == rightIndex)
        {
            RightTankLeft();
        }
    }

    public void RightTankRight()
    {
        if (rightIndex == 6)
        {
            rightIndex = 0;
        }
        else
        {
            rightIndex++;
        }

        if (leftIndex == rightIndex)
        {
            RightTankRight();
        }
    }

    private void ButtonFadesToColor(GameObject button, Color targetColor)
    {
        Graphic graphic = button.GetComponent<Graphic>();
        Button btn = button.GetComponent<Button>();
        graphic.CrossFadeColor(targetColor, btn.colors.fadeDuration, true, true);
    }

}
