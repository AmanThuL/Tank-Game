using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnLocation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //add the game object to the spawn locations
        GameObject.Find("PowerUp Manager").GetComponent<PowerUps>().addToSpawnLocations(gameObject);
    }

}
