using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialTransition : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public enum State
    {
        View,
        Fade,
    }
    [SerializeField] float fadeDuration = 1f;

    public void TransitionText(State offOrOn, string text)
    {
        StartCoroutine(FadeAnim(offOrOn == State.View, text));
    }
    IEnumerator FadeAnim(bool on, string text)
    {
        textMesh.text = text;
        float t = 0;
        Color newColor;
        newColor = textMesh.color;
        if(!on)
        {
            t = 1;
            newColor.a = t;
            textMesh.color = newColor;
            while(t > 0)
            {
                t -= Time.deltaTime / fadeDuration;
                newColor.a = t;
                textMesh.color = newColor;
                yield return null;
            }
            newColor.a = 0;
            textMesh.color = newColor;
        }
        else
        {
            t = 0;
            newColor.a = t;
            textMesh.color = newColor;
            while(t < 1)
            {
                t += Time.deltaTime / fadeDuration;
                newColor.a = t;
                textMesh.color = newColor;
                yield return null;
            }
            newColor.a = 1;
            textMesh.color = newColor;
        }
    }
}
