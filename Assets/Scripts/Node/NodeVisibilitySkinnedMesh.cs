using UnityEngine;

public class NodeVisibilitySkinnedMesh : MonoBehaviour
{
    private SkinnedMeshRenderer _renderer;
    [SerializeField] GridAgent _gridAgent;

    private int _visibilityRadius = 11;
    void Start()
    {
        _renderer = GetComponent<SkinnedMeshRenderer>();
        DistanceCheck(Player.instance.gridAgent.node);
    }
    void OnEnable()
    {
        PlayerGridAgent.OnPlayerMovement += DistanceCheck;
    }
    void OnDisable()
    {
        PlayerGridAgent.OnPlayerMovement -= DistanceCheck;
    }
    void DistanceCheck(NodeClass playerNode)
    {
        if(playerNode == null) print("NUULL");
        if(_gridAgent.node == null) print("NULLL2");
        int xD = Mathf.Abs(playerNode.nodeX - _gridAgent.node.nodeX);
        int yD = Mathf.Abs(playerNode.nodeY - _gridAgent.node.nodeY);
        int distance = xD + yD;
        _renderer.enabled = distance < _visibilityRadius;
    }
}
