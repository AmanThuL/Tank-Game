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

    Sprite[] sprites;

    int rightIndex;
    int leftIndex;

    void Start()
    {
        leftIndex = 0;
        rightIndex = 1;

        sprites = new Sprite[6];
        sprites[0] = redTank;
        sprites[1] = blueTank;
        sprites[2] = violetTank;
        sprites[3] = greenTank;
        sprites[4] = yellowTank;
        sprites[5] = orangeTank;
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
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            LeftTankRight();
            GameStats.player1Color = sprites[leftIndex];
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RightTankLeft();
            GameStats.player2Color = sprites[rightIndex];
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RightTankRight();
            GameStats.player2Color = sprites[rightIndex];
        }
    }

    void ChangeSprites()
    {
        leftTank.GetComponent<SpriteRenderer>().sprite = sprites[leftIndex];
        rightTank.GetComponent<SpriteRenderer>().sprite = sprites[rightIndex];
    }

    public void LeftTankLeft()
    {
        if (leftIndex == 0)
        {
            leftIndex = 5;
        }
        else
        {
            leftIndex--;
        }
    }

    public void LeftTankRight()
    {
        if (leftIndex == 5)
        {
            leftIndex = 0;
        }
        else
        {
            leftIndex++;
        }
    }

    public void RightTankLeft()
    {
        if (rightIndex == 0)
        {
            rightIndex = 5;
        }
        else
        {
            rightIndex--;
        }
    }

    public void RightTankRight()
    {
        if (rightIndex == 5)
        {
            rightIndex = 0;
        }
        else
        {
            rightIndex++;
        }
    }

}
