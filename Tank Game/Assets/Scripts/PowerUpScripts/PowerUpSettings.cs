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
    public Sprite speedUpOn;
    public Sprite speedUpOff;
    public Sprite doubleShotOn;
    public Sprite doubleShotOff;
    public Sprite infAmmoOn;
    public Sprite infAmmoOff;
    public Sprite shieldOn;
    public Sprite shieldOff;


    // Start is called before the first frame update
    void Start()
    {

        if (!GameStats.Instance.speedUp)
        {
            speedUpButton.GetComponent<Image>().sprite = speedUpOff;
        }

        if (!GameStats.Instance.doubleShot)
        {
            doubleShotButton.GetComponent<Image>().sprite = doubleShotOff;
        }

        if (!GameStats.Instance.infAmmo)
        {
            infAmmoButton.GetComponent<Image>().sprite = infAmmoOff;
        }

        if (!GameStats.Instance.shield)
        {
            shieldButton.GetComponent<Image>().sprite = shieldOff;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpeedUpChange()
    {
        if (GameStats.Instance.speedUp)
        {
            GameStats.Instance.speedUp = false;
            speedUpButton.GetComponent<Image>().sprite = speedUpOff;

        }
        else
        {
            GameStats.Instance.speedUp = true;
            speedUpButton.GetComponent<Image>().sprite = speedUpOn;
        }
    }

    public void DoubleShotChange()
    {
        if (GameStats.Instance.doubleShot)
        {
            GameStats.Instance.doubleShot = false;
            doubleShotButton.GetComponent<Image>().sprite = doubleShotOff;

        }
        else
        {
            GameStats.Instance.doubleShot = true;
            doubleShotButton.GetComponent<Image>().sprite = doubleShotOn;
        }
    }

    public void InfAmmoChange()
    {
        if (GameStats.Instance.infAmmo)
        {
            GameStats.Instance.infAmmo = false;
            infAmmoButton.GetComponent<Image>().sprite = infAmmoOff;

        }
        else
        {
            GameStats.Instance.infAmmo = true;
            infAmmoButton.GetComponent<Image>().sprite = infAmmoOn;
        }
    }

    public void ShieldChange()
    {
        if (GameStats.Instance.shield)
        {
            GameStats.Instance.shield = false;
            shieldButton.GetComponent<Image>().sprite = shieldOff;

        }
        else
        {
            GameStats.Instance.shield = true;
            shieldButton.GetComponent<Image>().sprite = shieldOn;
        }
    }

}
