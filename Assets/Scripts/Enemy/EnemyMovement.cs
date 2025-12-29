using System;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GridAgent gridAgent;
    public float duration;
    public void MoveAI(Action<CharacterStateMachine.State> onCompleteAction)
    {
        FindPath(onCompleteAction);
    }
    void FindPath(Action<CharacterStateMachine.State> onCompleteAction)
    {
        Pathfinder newPath = new Pathfinder();
        NodeClass targetNode = Player.instance.gridAgent.node;
        NodeClass bestNode = newPath.FindPath(gridAgent.node, targetNode);
        Vector3 start = gridAgent.node.worldPos;

        gridAgent.SetNode(bestNode);
        StartCoroutine(EnemyAnim(start, bestNode.worldPos, onCompleteAction));
    }
    IEnumerator EnemyAnim(Vector3 start, Vector3 goal, Action<CharacterStateMachine.State> onCompleteAction)
    {
        float t = 0;
        //run animation
        Vector3 direction = goal - start;
        direction.y = 0f;
        gridAgent.transform.forward = direction.normalized;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(start, goal, t);
            yield return null;
        }
        if(t > 1)
        {

            transform.position = goal;
            onCompleteAction.Invoke(CharacterStateMachine.State.Aggro);
            TurnManager.instance.ChangeBattleTurn(TurnManager.BattleState.PlayerTurn);
        }
    }
}