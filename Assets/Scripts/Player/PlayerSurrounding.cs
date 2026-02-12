using UnityEngine;

public class PlayerSurrounding : MonoBehaviour
{
    public enum CheckType
    {
        Enemy,
        ActiveEnemy,
        DeadEnemy,
    }
    public CheckType type;
    public bool CheckFor(CheckType type)
    {
        int[,] dir = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
        switch (type)
        {
            case CheckType.ActiveEnemy:
                for (int i = 0; i < dir.GetLength(0); i++)
                {
                    int x = Player.instance.gridAgent.nodeX + dir[i, 0];
                    int y = Player.instance.gridAgent.nodeY + dir[i, 1];
                    NodeClass neighbor = Node.instance.nodeGrid.grid[x, y];
                    if (neighbor.state == NodeClass.State.Enemy)
                        if (neighbor.enemyController.enemyState.state == EnemyState.State.Active)
                            return true;
                }
                return false;
            default: return false;
        }
    }
}
