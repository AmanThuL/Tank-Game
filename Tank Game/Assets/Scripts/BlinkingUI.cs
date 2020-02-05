using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingUI : MonoBehaviour
{
    private Image image;
    [SerializeField] private float blinkingRate;
    [SerializeField] private int blinkingTimes;
    [SerializeField] private int blinkingMaxCounts;

    public int SetBlinkingTimes { set => blinkingTimes = value; }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        StartBlinking();
    }

    void Update()
    {
    }

    private IEnumerator Blink()
    {
        while (blinkingTimes <= blinkingMaxCounts)
        {
            switch (image.color.a.ToString())
            {
                case "0":
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
                    blinkingTimes++;
                    yield return new WaitForSeconds(blinkingRate);
                    break;
                case "1":
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
                    yield return new WaitForSeconds(blinkingRate);
                    break;
            }
        }
    }

    public void StartBlinking()
    {
        StopCoroutine(Blink());
        StartCoroutine(Blink());
    }

    public void StopBlinking()
    {
        StopCoroutine(Blink());
    }
}
