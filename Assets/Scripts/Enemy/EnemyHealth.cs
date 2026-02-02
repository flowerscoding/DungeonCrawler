using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public EnemyData enemyData;
    public int curHealth {get; private set;}
    public int curStagger {get; private set;}
    [SerializeField] Canvas _healthBarCanvas;
    public RectTransform healthBar;
    [SerializeField] Canvas _staggerBarCanvas;
    public Image staggerBar;
    public Material staggerBarHighlight;
    float _prevHealthPercent;
    float _prevStagPercent;
    void Awake()
    {
        curHealth = enemyData.maxHealth;
    }
    public void TakeDamage(int damage)
    {
        _healthBarCanvas.enabled = true;
        _prevHealthPercent = (float)curHealth / enemyData.maxHealth;
        curHealth -= damage;
        StartCoroutine(HealthBarAnimation());
    }
    IEnumerator HealthBarAnimation()
    {
        float healthPercent = (float)curHealth / enemyData.maxHealth;
        float t = 0;
        float duration = 0.3f;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            float dynamicHealth = Mathf.Lerp(_prevHealthPercent, healthPercent, t);
            healthBar.localScale = new Vector3(dynamicHealth, 1, 1);
            yield return null;
        }
        healthBar.localScale = new Vector3(healthPercent, 1, 1);
    }
    public void TakeStagger(int amount)
    {
        _staggerBarCanvas.enabled = true;
        _prevStagPercent = (float)curStagger / enemyData.maxStagger;
        curStagger += amount;
        StartCoroutine(StaggerBarAnimation());
    }
    IEnumerator StaggerBarAnimation()
    {
        float staggerPercent = (float)curStagger / enemyData.maxStagger;
        float t = 0;
        float duration = 0.3f;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            float dynamicStagger = Mathf.Lerp(_prevStagPercent, staggerPercent, t);
            staggerBar.fillAmount = dynamicStagger;
            yield return null;
        }
        staggerBar.fillAmount = staggerPercent;
        if(staggerPercent >= 1)
            staggerBar.material = staggerBarHighlight;
    }
    public void ResetHealth(int newHealth) //for resetting the enemy
    {
        curStagger = 0;
        curHealth = newHealth;
        _healthBarCanvas.enabled = false;
        _staggerBarCanvas.enabled = false;
        staggerBar.material = null;
    }
}
