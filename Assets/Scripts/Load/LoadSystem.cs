using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSystem : MonoBehaviour
{
    public static LoadSystem instance;
    public Image transitionImg;
    public float transitionDuration;
    public float transitionPause;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(TransitionEffect(sceneName));
    }
    IEnumerator TransitionEffect(string sceneName)
    {
        Color c = new Color();
        float progress = 0;
        while (progress < 1)
        {
            progress += Time.deltaTime / transitionDuration;

            float alpha = Mathf.Clamp(progress, 0, 1);
            c = transitionImg.color;
            c.a = alpha;
            transitionImg.color = c;

            yield return null;
        }
        c.a = 1;
        transitionImg.color = c;
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(transitionPause);
    }
}

