using UnityEngine;

public class ResetDestructible : MonoBehaviour, SceneSystem.ResetScript
{
    [SerializeField] DestructibleController controller;
    void Start()
    {
        SceneSystem.instance.Register(this);

    }
    public void ResetState()
    {
        controller.ResetDestructible();
    }
}
