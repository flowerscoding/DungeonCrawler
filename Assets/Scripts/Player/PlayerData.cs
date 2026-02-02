using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour
{
    public int curHealth;
    public int maxHealth;

    public int attackStat;
    public int defenseStat;

    public float attackHitPoint;
    public float destroyHitPoint;
    public Image healthBar;
    [System.Serializable]
    public struct WeaponInstance
    {
        public WeaponData data;
    }
    public WeaponInstance weapon;

    float _prevHealthPercent;
    public void TakeDamage(int damage)
    {
        _prevHealthPercent = (float)curHealth / maxHealth;
        curHealth -= damage;
        if(curHealth > 0)
            Player.instance.StateChange(PlayerState.State.Hurt);
        else if(curHealth <= 0)
            Player.instance.StateChange(PlayerState.State.Dead);
        
        StartCoroutine(HealthBarAnimation());
    }
    IEnumerator HealthBarAnimation()
    {
        float t = 0;
        float duration = 0.3f;
        float healthPercent = (float)curHealth / maxHealth;
        while(t < 1)
        {
            t += Time.deltaTime / duration;
            float dynamicHealthPercent = Mathf.Lerp(_prevHealthPercent, healthPercent, t);

            healthBar.fillAmount = dynamicHealthPercent;
            yield return null;
        }
        healthBar.fillAmount = healthPercent;
    }
}