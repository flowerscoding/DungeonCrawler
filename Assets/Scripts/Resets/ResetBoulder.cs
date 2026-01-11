using UnityEngine;

public class ResetBoulder : MonoBehaviour, SceneSystem.ResetScript
{
    [SerializeField] BoulderController _controller;
    NodeClass _originNode;
    void Start()
    {
        SceneSystem.instance.Register(this);
        _originNode = _controller.gridAgent.node;
    }
    public void ResetState()
    {
        transform.position = _originNode.worldPos;
        _controller.ResetBoulder(_originNode);
    }
}
