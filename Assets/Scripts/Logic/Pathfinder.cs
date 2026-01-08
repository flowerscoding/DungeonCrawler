using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    public NodeClass FindPath(NodeClass startNode, NodeClass goalNode)
    {
        List<NodeClass> neighborNodes = new List<NodeClass>();
        int[,] dirs = new int[,]
        {
            {0, 1},
            {0, -1},
            {1, 0},
            {-1, 0},
        };
        for(int i = 0; i < 4; i++)
        {
            int nodeX = dirs[i, 0] + startNode.nodeX;
            int nodeY = dirs[i, 1] + startNode.nodeY;
            int gridSize = Node.instance.nodeGrid.gridSize;
            if(nodeX > 0 && nodeX < gridSize
                && nodeY > 0 && nodeY < gridSize)
            {
                NodeClass neighborNode = Node.instance.nodeGrid.grid[nodeX, nodeY];

                if(neighborNode.state != NodeClass.State.Empty)
                {
                    if(neighborNode.state == NodeClass.State.Player)
                    {
                        startNode.gCost = 0;
                        startNode.hCost = 0;
                        neighborNodes.Add(startNode);
                        return startNode;
                    }
                    continue; 
                }

                neighborNodes.Add(neighborNode);

                neighborNode.gCost = 10;
                int xDist = Mathf.Abs(neighborNode.nodeX - goalNode.nodeX);
                int yDist = Mathf.Abs(neighborNode.nodeY - goalNode.nodeY);
                neighborNode.hCost = xDist + yDist;
            }
        }
        NodeClass bestNode = CheckNeighbors(neighborNodes);
        return bestNode;
    }
    NodeClass CheckNeighbors(List<NodeClass> neighborNodes)
    {
        NodeClass bestNode = null; //best fcost. if tied best hcost
        int bestFCost = 0;
        foreach(NodeClass node in neighborNodes)
        {
            if(bestFCost == 0)
            {
                bestNode = node;
                bestFCost = node.fCost;
                continue;
            }
            if(node.fCost < bestFCost)
            {
                bestNode = node;
                bestFCost = node.fCost;
            }
        }
        return bestNode;
    }
}
