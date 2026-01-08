using UnityEngine;

public class NodeVisibility : MonoBehaviour
{
    private MeshRenderer _renderer;
    public NodeClass node;
    public int nodeX;
    public int nodeY;

    private int _visibilityRadius = 11;
    void Awake()
    {
        SetReferenceNode();
        _renderer = GetComponent<MeshRenderer>();
    }
    void OnEnable()
    {
        PlayerGridAgent.OnPlayerMovement += DistanceCheck;
        PlayerGridAgent.OnPlayerNodeSet += DistanceCheck;
    }
    void OnDisable()
    {
        PlayerGridAgent.OnPlayerMovement -= DistanceCheck;
        PlayerGridAgent.OnPlayerNodeSet -= DistanceCheck;
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
