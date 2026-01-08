using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    public Vector3 spawnPos;
    public Vector3 gridOrigin;
    public int gridSize;
    public float offSet;
    public NodeClass[,] grid { get; private set; }
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
        int x = Mathf.FloorToInt(spawnPos.x - gridOrigin.x);
        int y = Mathf.FloorToInt(spawnPos.z - gridOrigin.z);
        NodeClass node = Node.instance.nodeGrid.grid[x, y];
        Player.instance.gridAgent.SetStartNode(node);
        Player.instance.InitializeStartNode(node);
    }
}
