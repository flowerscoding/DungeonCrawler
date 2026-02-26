using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    public Vector3 spawnPos;
    public Vector3 gridOrigin;
    public int gridSize;
    public float offSet;
    public NodeClass[,] grid { get; private set; }
    void Awake()
    {
        CreateGrid();
        AssignPlayer();
    }
    public void CreateGrid()
    {
        grid = new NodeClass[gridSize, gridSize];
        float xOff = gridOrigin.x;
        float zOff = gridOrigin.z;
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 worldPos = new Vector3(xOff, 0, zOff);
                grid[x, y] = new NodeClass(worldPos, x, y);
                zOff += offSet;
            }
            zOff = gridOrigin.z;
            xOff += offSet;
        }
    }
    public void AssignPlayer()
    {
        string spawnPoint = Player.instance.playerData.spawnPoint;
        if (spawnPoint == null)
        {
            int x = Mathf.FloorToInt(spawnPos.x - gridOrigin.x);
            int y = Mathf.FloorToInt(spawnPos.z - gridOrigin.z);
            NodeClass node = Node.instance.nodeGrid.grid[x, y];
            Player.instance.gridAgent.SetStartNode(node);
            Player.instance.InitializeStartNode(node);
            return;
        }
        if (spawnPoint != null)
        {
            Transform spawn;
            if (GameObject.Find(spawnPoint))
            {
                spawn = GameObject.Find(spawnPoint).transform;
                Player.instance.SpawnPointUpdate(spawnPoint);
                int x2 = Mathf.FloorToInt(spawn.position.x - gridOrigin.x);
                int y2 = Mathf.FloorToInt(spawn.position.z - gridOrigin.z);
                NodeClass node2 = Node.instance.nodeGrid.grid[x2, y2];
                Vector3 origPos = node2.worldPos;
                node2.worldPos = spawn.transform.position;
                Player.instance.gridAgent.SetStartNode(node2);
                Player.instance.InitializeStartNode(node2);
                node2.worldPos = origPos; //to erase any humps created by gridagent offsets
            }
        }
    }
}
