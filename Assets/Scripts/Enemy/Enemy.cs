using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy instance;
    public EnemySystem enemySystem;
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void ActivateEnemy(EnemyController controller)
    {
        enemySystem.ActivateEnemy(controller);
    }
    public void PauseCharges(bool pause)
    {
        enemySystem.PauseCharges(pause);
    }
}
