using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Toggle speedUpToggle;
    public Toggle infAmmoToggle;
    public Toggle limitedAmmoToggle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameStats.speedUp = speedUpToggle.isOn;
        GameStats.infAmmo = infAmmoToggle.isOn;
        GameStats.limitedAmmo = limitedAmmoToggle.isOn;
    }
}
