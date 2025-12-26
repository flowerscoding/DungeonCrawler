using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GridAgent gridAgent;
    public float duration;
    public void MoveAI()
    {
        FindPath();
    }
    void FindPath()
    {
        Pathfinder newPath = new Pathfinder();
        NodeClass targetNode = Player.instance.playerGridAgent.node;
        NodeClass bestNode = newPath.FindPath(gridAgent.node, targetNode);
        Vector3 start = gridAgent.node.worldPos;

        gridAgent.SetNode(bestNode);
        StartCoroutine(EnemyAnim(start, bestNode.worldPos));
    }
    IEnumerator EnemyAnim(Vector3 start, Vector3 goal)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(start, goal, t);
            yield return null;
        }
        if(t > 1)
            transform.position = goal;
    }
}