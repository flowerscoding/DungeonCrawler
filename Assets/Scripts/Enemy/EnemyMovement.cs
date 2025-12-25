using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform testEnemy;
    public void MoveEnemy()
    {
        int nodeX = Mathf.FloorToInt(testEnemy.position.x);
        int nodeY = Mathf.FloorToInt(testEnemy.position.z);
        nodeX += Mathf.FloorToInt(Node.instance.nodeGrid.gridOrigin.x);
        nodeY += Mathf.FloorToInt(Node.instance.nodeGrid.gridOrigin.z);
        NodeClass curNode = Node.instance.nodeGrid.grid[nodeX, nodeY];
    }
}
