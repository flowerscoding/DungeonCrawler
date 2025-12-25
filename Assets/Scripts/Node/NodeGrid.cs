using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    public Vector3 gridOrigin;
    public int gridSize;
    public float offSet;
    public NodeClass[,] grid {get; private set;}
    void Awake()
    {
        CreateGrid();
    }
    void CreateGrid()
    {
        grid = new NodeClass[gridSize, gridSize];
        float xOff = 0;
        float zOff = 0;
        for(int x = 0; x < gridSize; x++)
        {
            for(int y = 0; y < gridSize; y++)
            {
                Vector3 worldPos = gridOrigin + new Vector3(xOff, 0, zOff);
                grid[x, y] = new NodeClass(worldPos, x, y);
                zOff = y + offSet;
            }
            xOff = x + offSet;
        }
    }
}
