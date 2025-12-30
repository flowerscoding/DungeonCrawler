using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GridAgent gridAgent;
    public void MoveAI(EnemyController controller)
    {
        FindPath(controller);
    }
    void FindPath(EnemyController controller)
    {
        Pathfinder newPath = new Pathfinder();
        NodeClass targetNode = Player.instance.gridAgent.node;
        NodeClass bestNode = newPath.FindPath(gridAgent.node, targetNode);
        Vector3 start = gridAgent.node.worldPos;

        gridAgent.SetNode(bestNode);
        StartCoroutine(EnemyAnim(start, bestNode.worldPos, controller));
    }
    IEnumerator EnemyAnim(Vector3 start, Vector3 goal, EnemyController controller)
    {
        yield return null;
        float t = 0;
        Vector3 direction = goal - start;
        direction.y = 0f;
        gridAgent.transform.forward = direction.normalized;
        while (t < 1)
        {
            t += Time.deltaTime / TurnManager.instance.movementDuration;
            transform.position = Vector3.Lerp(start, goal, t);
            yield return null;
        }
        if (t > 1)
        {

            transform.position = goal;
            controller.NewState(EnemyAI.State.Aggro);
        }
    }
}