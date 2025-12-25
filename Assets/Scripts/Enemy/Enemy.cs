using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy instance;
    public EnemyMovement enemyMovement;
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
}
