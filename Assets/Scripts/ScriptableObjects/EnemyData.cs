using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public int attackDamage;

    public float attackHitFrame; //not actually a frame just the point in progress of the animation the attack lands on the enemy. used for attack/dmg visuals
}
