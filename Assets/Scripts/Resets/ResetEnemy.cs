using UnityEngine;

public class ResetEnemy : MonoBehaviour, SceneSystem.ResetScript
{
    [SerializeField] EnemyController _enemyController;
    private Vector3 _resetSpot;
    void Awake()
    {
        _resetSpot = transform.position;
    }
    void Start()
    {
        SceneSystem.instance.Register(this);
    }
    public void ResetState()
    {
        transform.position = _resetSpot;
        _enemyController.ResetEnemy();
    }
}
