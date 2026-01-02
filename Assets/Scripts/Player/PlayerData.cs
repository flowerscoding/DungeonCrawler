using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int curHealth;

    public int attackStat;
    public int defenseStat;

    public float attackHitPoint;
    [System.Serializable]
    public struct WeaponInstance
    {
        public WeaponData data;

    }
    public WeaponInstance weapon;
}
