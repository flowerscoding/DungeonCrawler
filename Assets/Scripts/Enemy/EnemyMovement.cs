using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform testEnemy;
    private NodeClass _startNode;
    private NodeClass _targetNode;

    private List<NodeClass> neighborNodes = new List<NodeClass>();
    public void MoveEnemy()
    {
        int nodeX = Mathf.FloorToInt(testEnemy.position.x + Node.instance.nodeGrid.gridOrigin.x);
        int nodeY = Mathf.FloorToInt(testEnemy.position.z + Node.instance.nodeGrid.gridOrigin.z);
        nodeX += Mathf.FloorToInt(Node.instance.nodeGrid.gridOrigin.x);
        nodeY += Mathf.FloorToInt(Node.instance.nodeGrid.gridOrigin.z);
        _startNode = Node.instance.nodeGrid.grid[nodeX, nodeY];
    }
    void AStarPathfinding()
    {
        int[,] dirs = new int[,]
        {
            {0, 1},
            {0, -1},
            {1, 0},
            {-1, 0},
        };
        for(int i = 0; i < 4; i++)
        {
            int nodeX = dirs[i, 0] + _startNode.indexX;
            int nodeY = dirs[i, 1] + _startNode.indexY;
            int gridSize = Node.instance.nodeGrid.gridSize;
            if(nodeX > 0 && nodeX < gridSize
                && nodeY > 0 && nodeY < gridSize)
            {
                NodeClass neighborNode = Node.instance.nodeGrid.grid[nodeX, nodeY];
                neighborNodes.Add(neighborNode);
            }
        }
    }
    void CheckNeighbors()
    {
        NodeClass bestNode;
        int bestFCost;
        int bestHCost;
        foreach(NodeClass node in neighborNodes)
        {
        }
    }
}