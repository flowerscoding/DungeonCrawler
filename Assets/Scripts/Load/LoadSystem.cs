using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSystem : MonoBehaviour
{
    public static LoadSystem instance;
    public static event System.Action OnLoad;
    public Image transitionImg;
    public enum SceneType
    {
        IntroArea,
        Castle_Floor1,
    }
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void LoadScene(SceneType scene)
    {
        string sceneName = scene.ToString();
        StartCoroutine(TransitionEffect(sceneName));
    }
    IEnumerator TransitionEffect(string sceneName)
    {
        Color c = new Color();
        float progress = 0;
        float duration = TransitionData.TransitionTime;
        float pauseTime = TransitionData.TransitionPause;
        while (progress < 1)
        {
            progress += Time.deltaTime / duration;

            float alpha = Mathf.Clamp(progress, 0, 1);
            c = transitionImg.color;
            c.a = alpha;
            transitionImg.color = c;

            yield return null;
        }
        c.a = 1;
        transitionImg.color = c;
        SceneManager.LoadScene(sceneName);
        TurnManager.instance.ChangeTurn(TurnManager.State.Resolving);
        OnLoad?.Invoke(); //world signal
        
        yield return new WaitForSeconds(pauseTime);
        Player.instance.StateChange(PlayerState.State.Idle);

        progress = 0;
        while (progress < 1)
        {
            progress += Time.deltaTime / duration;

            float alpha = Mathf.Clamp(progress, 0, 1);
            c = transitionImg.color;
            c.a = 1 - alpha;
            transitionImg.color = c;

            yield return null;
        }
        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
         c.a = 0;
        transitionImg.color = c;
    }
}

