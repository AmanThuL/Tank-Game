using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;

    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeOutTime;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    /// <summary>
    /// Fade to a certain scene in game
    /// </summary>
    /// <param name="scene">Scene to be transitioned to</param>
    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = fadeInTime;

        while (t > 0f) // Keep animating until t reaches 0
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0; // Skip to the next frame
        }

        Time.timeScale = 1;

        gameObject.SetActive(false);
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0;

        while (t < fadeOutTime) // Keep animating until t reaches 0
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0; // Skip to the next frame
        }

        // Load the scene
        SceneManager.LoadScene(scene);
    }
}