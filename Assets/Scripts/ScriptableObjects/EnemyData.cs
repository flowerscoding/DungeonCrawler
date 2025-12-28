using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public int attack;

    public float idleDuration;
    public float walkDuration;
    public float attackDuration;
    public float hurtDuration;
    public float deadDuration;
}
