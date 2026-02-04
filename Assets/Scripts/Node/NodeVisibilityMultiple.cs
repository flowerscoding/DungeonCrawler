using UnityEngine;

public class NodeVisibilityMultiple : MonoBehaviour
{
    [SerializeField] MeshRenderer[] _meshRenderers;
    [SerializeField] SkinnedMeshRenderer[] _skinnedMeshRenderers;
    [SerializeField] ParticleSystem[] _particleSystems;
    [SerializeField] Light[] _lights;
    public NodeClass node;
    public int nodeX;
    public int nodeY;
    private int _visibilityRadius = 11;
    void Awake()
    {
        SetReferenceNode();
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
    void SetReferenceNode()
    {
        nodeX = Mathf.RoundToInt(transform.position.x - Node.instance.nodeGrid.gridOrigin.x);
        nodeY = Mathf.RoundToInt(transform.position.z - Node.instance.nodeGrid.gridOrigin.z);
        node = Node.instance.nodeGrid.grid[nodeX, nodeY];
    }
    void DistanceCheck(NodeClass playerNode)
    {
        int xD = Mathf.Abs(playerNode.nodeX - nodeX);
        int yD = Mathf.Abs(playerNode.nodeY - nodeY);
        int distance = xD + yD;
        bool visible = distance < _visibilityRadius;
        foreach (MeshRenderer mesh in _meshRenderers)
            mesh.enabled = visible;
        foreach (SkinnedMeshRenderer mesh in _skinnedMeshRenderers)
            mesh.enabled = visible;
        foreach (Light light in _lights)
            light.enabled = visible;
        if (visible)
            foreach (ParticleSystem ps in _particleSystems)
                ps.Play();
        else
            foreach (ParticleSystem ps2 in _particleSystems)
                ps2.Stop();
    }
}
