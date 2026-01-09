using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
    IEnumerator EnemyAnim(Vector3 start, Vector3 goal, EnemyController controller, NodeClass curNode)
    {
        yield return null;
        float t = 0;
        Vector3 direction = goal - start;
        direction.y = 0f;
        if(direction.normalized != Vector3.zero)
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