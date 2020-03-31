using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    private Text num;

    // Start is called before the first frame update
    void Start()
    {
        num = GetComponent<Text>();
        StartCoroutine(Countdown(3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        yield return new WaitForSeconds(0.5f);
        while (count > 0)
        {
            // display something...
            num.text = count.ToString();

            yield return new WaitForSeconds(1);
            count--;
        }

        // count down is finished...
        num.text = "GO!";

        yield return new WaitForSeconds(1f);
        GameStats.isInputEnabled = true;
        gameObject.SetActive(false);
    }
}
