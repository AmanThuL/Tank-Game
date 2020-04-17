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

    //Cancel symbols
    public Image speedUpCancel;
    public Image doubleShotCancel;
    public Image infAmmoCancel;
    public Image shieldCancel;


    // Start is called before the first frame update
    void Start()
    {
        speedUpButton.onClick.AddListener(SpeedUpCheck);
        doubleShotButton.onClick.AddListener(DoubleShotCheck);
        infAmmoButton.onClick.AddListener(InfAmmoCheck);
        shieldButton.onClick.AddListener(ShieldCheck);
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
            speedUpCancel.enabled = true;
        }
        else
        {
            GameStats.Instance.speedUp = true;
            speedUpCancel.enabled = false;
        }
    }

    void DoubleShotCheck()
    {
        if (GameStats.Instance.doubleShot)
        {
            GameStats.Instance.doubleShot = false;
            doubleShotCancel.enabled = true;
        }
        else
        {
            GameStats.Instance.doubleShot = true;
            doubleShotCancel.enabled = false;
        }
    }

    void InfAmmoCheck()
    {
        if (GameStats.Instance.infAmmo)
        {
            GameStats.Instance.infAmmo = false;
            infAmmoCancel.enabled = true;
        }
        else
        {
            GameStats.Instance.infAmmo = true;
            infAmmoCancel.enabled = false;
        }
    }

    void ShieldCheck()
    {
        if (GameStats.Instance.shield)
        {
            GameStats.Instance.shield = false;
            shieldCancel.enabled = true;
        }
        else
        {
            GameStats.Instance.shield = true;
            shieldCancel.enabled = false;
        }
    }

}
