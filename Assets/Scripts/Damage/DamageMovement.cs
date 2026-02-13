using System.Collections;
using UnityEngine;
using TMPro;

public class DamageMovement : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public float fadeStart;
    public float fadeDuration;
    float rotationSpeed;
    public float textSpeed;
    public float frequency;
    public float amplitude;
    bool fade;
    void Update()
    {
        if(!fade)
        {
            fade = true;
            StartCoroutine(Fade());
        }
        transform.position += Vector3.up * textSpeed * Time.deltaTime;
        rotationSpeed = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + rotationSpeed));
    }
    IEnumerator Fade()
    {

        float progress = 0;
        while (progress < 1)
        {
            progress += Time.deltaTime / fadeStart;
            yield return null;
        }
        progress = 0;
        Color c = damageText.color;
        while (progress < 1)
        {
            progress += Time.deltaTime / fadeDuration;
            c.a = 1 - progress;
            damageText.color = c;
            yield return null;
        }
        c.a = 0;
        damageText.color = c;
        Destroy(transform.parent.gameObject);
    }
}
