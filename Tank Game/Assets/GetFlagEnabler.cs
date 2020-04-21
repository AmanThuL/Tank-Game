using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFlagEnabler : MonoBehaviour
{
    void Start()
    {
        if (GameStats.Instance.mode != GameMode.Flag)
        {
            Destroy(gameObject);
        }
    }
}
