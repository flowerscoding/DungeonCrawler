using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class EnemyMovement : MonoBehaviour
{
    public GridAgent gridAgent;
    private List<NodeClass> goalNodes = new List<NodeClass>();
    private int _enemySlowCounter = 0;


    public void MoveAI(EnemyController controller)
    {
        if (Player.instance.playerMovement.runToggle)
        {
            if (_enemySlowCounter++ % 2 != 0)
            {

                return;
            }
        }
        else
            _enemySlowCounter = 0;
        FindPath(controller);
    }
    void FindPath(EnemyController controller)
    {
        Pathfinder newPath = new Pathfinder();
        NodeClass targetNode = Player.instance.gridAgent.node;
        NodeClass bestNode = newPath.FindPath(gridAgent.node, targetNode);
        Vector3 start = gridAgent.node.worldPos;

        gridAgent.SetNode(bestNode);

        goalNodes.Add(bestNode);

        if (goalNodes.Count == 1)
            StartCoroutine(EnemyAnim(start, bestNode.worldPos, controller, bestNode));
        
        CheckSurrounding(bestNode, controller);
    }
    public void CheckSurrounding(NodeClass standingNode, EnemyController controller)
    {
        int x = standingNode.nodeX;
        int y = standingNode.nodeY;
        int[,] dir = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 }, };
        bool playerFound = false;
        for (int i = 0; i < 4; i++)
        {
            int xD = dir[i, 0];
            int yD = dir[i, 1];
            NodeClass neighborNode = Node.instance.nodeGrid.grid[x + xD, y + yD];
            if(neighborNode.state == NodeClass.State.Player)
            {
                playerFound = true;
                break;
            }
            else
                playerFound = false;
        }
        controller.EnableAttackCharge(playerFound);
    }
    IEnumerator EnemyAnim(Vector3 start, Vector3 goal, EnemyController controller, NodeClass curNode)
    {
        yield return null;
        float t = 0;
        Vector3 direction = goal - start;
        direction.y = 0f;
        if (direction.normalized != Vector3.zero)
            gridAgent.transform.forward = direction.normalized;
        while (t < 1)
        {
            t += Time.deltaTime / TurnManager.instance.movementDuration;
            transform.position = Vector3.Lerp(start, goal, t);
            yield return null;
        }
        goalNodes.Remove(curNode);

        if (goalNodes.Count <= 0)
        {
            transform.position = goal;
            controller.NewState(EnemyAI.State.Aggro);
        }
        else
        {
            transform.position = goal;
            NodeClass newGoal = goalNodes[0];
            StartCoroutine(EnemyAnim(goal, newGoal.worldPos, controller, newGoal));
        }
    }
}