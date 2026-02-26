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
    public void DistanceCheck(NodeClass playerNode)
    {
        int xD = Mathf.Abs(playerNode.nodeX - nodeX);
        int yD = Mathf.Abs(playerNode.nodeY - nodeY);
        int distance = xD + yD;
        if(_renderer != null)
            _renderer.enabled = distance < _visibilityRadius;
    }
    void SetReferenceNode()
    {
        nodeX = Mathf.RoundToInt(transform.position.x - Node.instance.nodeGrid.gridOrigin.x);
        nodeY = Mathf.RoundToInt(transform.position.z - Node.instance.nodeGrid.gridOrigin.z);
        node = Node.instance.nodeGrid.grid[nodeX, nodeY];
    }
}
