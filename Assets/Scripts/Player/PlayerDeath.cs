using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    public Image blackScreen;
    public TextMeshProUGUI deathTmesh;
    public void DeathScreen()
    {
        
        StartCoroutine(deathCoroutine());
    }
    IEnumerator deathCoroutine()
    {
        float t = 0;
        float timer = 1.1f;
        float blackScreenDuration = 1f;
        float textDuration = 3f;
        Color c = blackScreen.color;
        Color c2 = deathTmesh.color;
        while (t < timer)
        {
            c.a = t / blackScreenDuration;
            blackScreen.color = c;
            t += Time.deltaTime;
            yield return null;
        }
        t = 0;
        while(t < 5)
        {
            c2.a = t / textDuration;
            deathTmesh.color = c2;
            t += Time.deltaTime;
            yield return null;
        }
    }
}
