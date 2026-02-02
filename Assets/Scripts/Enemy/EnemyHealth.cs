using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public EnemyData enemyData;
    public int curHealth {get; private set;}
    [SerializeField] Canvas _healthBarCanvas;
    public RectTransform healthBar;
    float _prevHealthPercent;
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
    public void SetNewHealth(int newHealth) //for resetting the enemy
    {
        curHealth = newHealth;
    }
}
