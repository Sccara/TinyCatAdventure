using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public AnimationCurve fadeCurve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }


    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = fadeCurve.Evaluate(t);
            fadeImage.color = new Color(0, 0, 0, a);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = fadeCurve.Evaluate(t);
            fadeImage.color = new Color(0, 0, 0, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
