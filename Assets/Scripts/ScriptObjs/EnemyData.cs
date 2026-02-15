using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public float maxStagger;
    [System.Serializable]
    public class Attack
    {
        public string attackName;
        public int attackDamage;
        public float chargeDuration;
        public int staggerValue;
    }
    public Attack[] attacks;
}