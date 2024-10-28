using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneMove : Singleton<SceneMove>
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    private void Start()
    {
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeIn());

    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeIn()
    {
        fadeImage.color = new Color(0, 0, 0, 1); // 검은색에서 시작
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, 1 - (timer / fadeDuration)); // 투명하게
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 0); // 완전히 투명
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        fadeImage.color = new Color(0, 0, 0, 0);
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, timer / fadeDuration);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 1); 

        SceneManager.LoadScene(sceneName); 

        yield return FadeIn();
    }
}