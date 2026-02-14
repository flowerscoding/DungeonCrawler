using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour
{
    public int curHealth;
    public int maxHealth;
    public int attackStat;
    public int defenseStat;
    public float staminaMax;
    public Vector3 worldPos;

    public float attackHitPoint;
    public float destroyHitPoint;
    public Image healthBar;

    public LoadSystem.Scene currentScene;
    [System.Serializable]
    public struct WeaponInstance
    {
        public WeaponData data;
    }
    public WeaponInstance weapon;

    float _prevHealthPercent;
    public void TakeDamage(int damage)
    {
        Color red = new Color(1, 0, 0);
        DamageManager.Instance.SpawnDamage(damage, Player.instance.playerMovement.playerRB.transform, red);
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
    public void LoadData()
    {
        currentScene = SaveSystem.Instance.currentData.scene;
        curHealth = SaveSystem.Instance.currentData.health;
        worldPos = SaveSystem.Instance.currentData.position;

        healthBar.fillAmount = (float) curHealth / maxHealth;
    }
    public void SceneUpdate(LoadSystem.Scene scene)
    {
        currentScene = scene;
    }
    public void UpdateWorldPos(Vector3 pos)
    {
        worldPos = pos;
    }
    public void Heal(int amount)
    {
        float prevHealth = curHealth;
        curHealth += amount;
        if(curHealth > maxHealth)
            curHealth = maxHealth;
        StartCoroutine(HealPlayer(prevHealth));
    }
    IEnumerator HealPlayer(float prevHealth)
    {
        float duration = 0.3f;
        while(prevHealth < curHealth)
        {
            prevHealth += Time.deltaTime / duration;
            healthBar.fillAmount = (float) prevHealth / maxHealth;
            yield return null;
        }
        healthBar.fillAmount = (float) curHealth / maxHealth;
    }
}