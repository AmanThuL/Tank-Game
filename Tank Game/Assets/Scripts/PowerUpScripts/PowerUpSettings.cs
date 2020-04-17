using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpSettings : MonoBehaviour
{
    //Buttons
    public GameObject speedUpButton;
    public GameObject doubleShotButton;
    public GameObject infAmmoButton;
    public GameObject shieldButton;

    //Colors
    ColorBlock offColor;
    ColorBlock onColor;


    // Start is called before the first frame update
    void Start()
    {
        offColor = ColorBlock.defaultColorBlock;
        offColor.normalColor = new Color(41, 41, 41);

        onColor = ColorBlock.defaultColorBlock;
        onColor.normalColor = new Color(255, 255, 255);

        speedUpButton.GetComponent<Button>().onClick.AddListener(SpeedUpCheck);
        doubleShotButton.GetComponent<Button>().onClick.AddListener(DoubleShotCheck);
        infAmmoButton.GetComponent<Button>().onClick.AddListener(InfAmmoCheck);
        shieldButton.GetComponent<Button>().onClick.AddListener(ShieldCheck);

        if (!GameStats.Instance.speedUp)
        {
            speedUpButton.GetComponent<Button>().colors = offColor;
        }

        if (!GameStats.Instance.doubleShot)
        {
            doubleShotButton.GetComponent<Button>().colors = offColor;
        }

        if (!GameStats.Instance.infAmmo)
        {
            infAmmoButton.GetComponent<Button>().colors = offColor;
        }

        if (!GameStats.Instance.shield)
        {
            shieldButton.GetComponent<Button>().colors = offColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpeedUpCheck()
    {
        //if (GameStats.Instance.speedUp)
        //{
        //    GameStats.Instance.speedUp = false;
        //    speedUpButton.GetComponent<Image>().color = new Color(41, 41, 41);
        //}
        //else
        //{
        //    GameStats.Instance.speedUp = true;
        //    speedUpButton.GetComponent<Button>().colors = onColor;
        //}

        GameStats.Instance.speedUp = !GameStats.Instance.speedUp;
        speedUpButton.GetComponent<Image>().color = GameStats.Instance.speedUp ? new Color(255, 255, 255) : new Color(141, 141, 141);
    }

    void DoubleShotCheck()
    {
        if (GameStats.Instance.doubleShot)
        {
            GameStats.Instance.doubleShot = false;
            doubleShotButton.GetComponent<Button>().colors = offColor;
        }
        else
        {
            GameStats.Instance.doubleShot = true;
            doubleShotButton.GetComponent<Button>().colors = onColor;
        }
    }

    void InfAmmoCheck()
    {
        if (GameStats.Instance.infAmmo)
        {
            GameStats.Instance.infAmmo = false;
            infAmmoButton.GetComponent<Button>().colors = offColor;
        }
        else
        {
            GameStats.Instance.infAmmo = true;
            infAmmoButton.GetComponent<Button>().colors = onColor;
        }
    }

    void ShieldCheck()
    {
        if (GameStats.Instance.shield)
        {
            GameStats.Instance.shield = false;
            shieldButton.GetComponent<Button>().colors = offColor;
        }
        else
        {
            GameStats.Instance.shield = true;
            shieldButton.GetComponent<Button>().colors = onColor;
        }
    }

}
