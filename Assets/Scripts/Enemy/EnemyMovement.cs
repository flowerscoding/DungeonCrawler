using System;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GridAgent gridAgent;
    public float duration;
    public void MoveAI(Action<CharacterStateMachine.State> onCompleteAction)
    {
        print(3);
        FindPath(onCompleteAction);
    }
    void FindPath(Action<CharacterStateMachine.State> onCompleteAction)
    {
        print(2);
        Pathfinder newPath = new Pathfinder();
        NodeClass targetNode = Player.instance.playerGridAgent.node;
        NodeClass bestNode = newPath.FindPath(gridAgent.node, targetNode);
        Vector3 start = gridAgent.node.worldPos;

        gridAgent.SetNode(bestNode);
        StartCoroutine(EnemyAnim(start, bestNode.worldPos, onCompleteAction));
    }
    IEnumerator EnemyAnim(Vector3 start, Vector3 goal, Action<CharacterStateMachine.State> onCompleteAction)
    {
        print(1);
        float t = 0;
        //run animation
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(start, goal, t);
            yield return null;
        }
        if(t > 1)
        {

            transform.position = goal;
            onCompleteAction.Invoke(CharacterStateMachine.State.Idle);
        }
    }
}