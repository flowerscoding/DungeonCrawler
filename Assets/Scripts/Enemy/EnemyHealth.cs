using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyData enemyData;
    public int curHealth;
    void Awake()
    {
        curHealth = enemyData.maxHealth;
    }
}
