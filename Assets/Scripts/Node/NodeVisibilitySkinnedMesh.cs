using UnityEngine;

public class NodeVisibilitySkinnedMesh : MonoBehaviour
{
    private SkinnedMeshRenderer _renderer;
    public NodeClass node;
    public int nodeX;
    public int nodeY;

    private int _visibilityRadius = 11;
    void Awake()
    {
        SetReferenceNode();
        _renderer = GetComponent<SkinnedMeshRenderer>();
    }
    void OnEnable()
    {
        PlayerGridAgent.OnPlayerMovement += DistanceCheck;
    }
    void OnDisable()
    {
        PlayerGridAgent.OnPlayerMovement -= DistanceCheck;
    }
    void SetReferenceNode()
    {
        nodeX = Mathf.FloorToInt(transform.position.x - Node.instance.nodeGrid.gridOrigin.x);
        nodeY = Mathf.FloorToInt(transform.position.z - Node.instance.nodeGrid.gridOrigin.z);
        node = Node.instance.nodeGrid.grid[nodeX, nodeY];
    }
    void DistanceCheck(NodeClass playerNode)
    {
        int xD = Mathf.Abs(playerNode.nodeX - nodeX);
        int yD = Mathf.Abs(playerNode.nodeY - nodeY);
        int distance = xD + yD;
        _renderer.enabled = distance < _visibilityRadius;
    }
}
