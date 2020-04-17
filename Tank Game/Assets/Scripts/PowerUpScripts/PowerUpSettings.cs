using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpSettings : MonoBehaviour
{
    //Buttons
    public Button speedUpButton;
    public Button doubleShotButton;
    public Button infAmmoButton;
    public Button shieldButton;

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

        speedUpButton.onClick.AddListener(SpeedUpCheck);
        doubleShotButton.onClick.AddListener(DoubleShotCheck);
        infAmmoButton.onClick.AddListener(InfAmmoCheck);
        shieldButton.onClick.AddListener(ShieldCheck);

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
        if (GameStats.Instance.speedUp)
        {
            GameStats.Instance.speedUp = false;
            speedUpButton.GetComponent<Button>().colors = offColor;
        }
        else
        {
            GameStats.Instance.speedUp = true;
            speedUpButton.GetComponent<Button>().colors = onColor;
        }
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
