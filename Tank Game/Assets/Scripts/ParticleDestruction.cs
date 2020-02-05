using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestruction : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(gameObject.GetComponent<ParticleSystem>().main.duration + 5);
        Destroy(gameObject);
    }
}
