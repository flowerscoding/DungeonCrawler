using UnityEngine;

public class NodeVisibilityLight : MonoBehaviour
{
    private Light _light;
    public NodeClass node;
    public int nodeX;
    public int nodeY;

    private int _visibilityRadius = 11;
    void Awake()
    {
        SetReferenceNode();
        _light= GetComponent<Light>();
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
        if (distance < _visibilityRadius)
            _light.enabled = true;
        else
            _light.enabled = false;
    }
}
