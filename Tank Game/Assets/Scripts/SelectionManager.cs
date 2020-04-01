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

    void Start()
    {
        leftIndex = 0;
        rightIndex = 1;

        sprites = new Sprite[7];
        sprites[0] = redTank;
        sprites[1] = blueTank;
        sprites[2] = violetTank;
        sprites[3] = greenTank;
        sprites[4] = yellowTank;
        sprites[5] = orangeTank;
        sprites[6] = cyanTank;
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
            LeftTankLeft();
            GameStats.player1Color = sprites[leftIndex];
            GameStats.player1TankColor = leftIndex;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            LeftTankRight();
            GameStats.player1Color = sprites[leftIndex];
            GameStats.player1TankColor = leftIndex;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RightTankLeft();
            GameStats.player2Color = sprites[rightIndex];
            GameStats.player2TankColor = rightIndex;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RightTankRight();
            GameStats.player2Color = sprites[rightIndex];
            GameStats.player2TankColor = rightIndex;
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

}
