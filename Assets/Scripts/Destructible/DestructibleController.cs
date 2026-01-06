using UnityEngine;

public class DestructibleController : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private GridAgent _gridAgent;
    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _gridAgent = GetComponent<GridAgent>();
    }
    public  void DestroyDestructible()
    {
        GetComponent<NodeVisibility>().enabled = false;
        _meshRenderer.enabled = false;
        _gridAgent.ChangeNodeState(NodeClass.State.Empty);
        _gridAgent.node.destructibleController = null;
    }
}
