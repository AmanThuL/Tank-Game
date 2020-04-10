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
        GameStats.Instance.speedUp = speedUpToggle.isOn;
        GameStats.Instance.infAmmo = infAmmoToggle.isOn;
        GameStats.Instance.limitedAmmo = limitedAmmoToggle.isOn;
    }
}
