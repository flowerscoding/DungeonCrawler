using UnityEngine;

public class DestructibleController : MonoBehaviour
{
    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] GridAgent _gridAgent;
    [SerializeField] NodeVisibility _nodeVisibility;
    public  void DestroyDestructible()
    {
        _nodeVisibility.enabled = false;
        _meshRenderer.enabled = false;
        _gridAgent.ChangeNodeState(NodeClass.State.Empty);
        _gridAgent.node.destructibleController = null;
    }
    public void ResetDestructible()
    {
        _nodeVisibility.enabled = true;
        _meshRenderer.enabled = true;
        _gridAgent.ChangeNodeState(NodeClass.State.Destructible);
        _gridAgent.node.destructibleController = this;
    }
}
