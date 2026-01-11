using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class BoulderState : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    
    public enum State
    {
        Idle,
        Interactable,
        Moving,
    }
    public State state;
    public void NewState(State newState, BoulderController controller)
    {
        state = newState;
        switch(state)
        {
            case State.Idle : IdleState(); break;
            case State.Interactable: InteractableState(); break;
            case State.Moving : MovingState(controller); break;
        }
    }
    void IdleState()
    {
        meshRenderer.materials[1].SetColor("_OutlineColor", Color.black);
    }
    void InteractableState()
    {
        meshRenderer.materials[1].SetColor("_OutlineColor", Color.white);
    }
    void MovingState(BoulderController controller)
    {
        meshRenderer.materials[1].SetColor("_OutlineColor", Color.black);
        NodeClass newNode = Player.instance.playerInteract.goalNode;
        Node.instance.ResetNode(newNode);
        newNode.state = NodeClass.State.Boulder;
        newNode.boulderController = controller;
        StartCoroutine(TrackMoving(newNode, controller));
        controller.gridAgent.SetNode(newNode);
    }
    IEnumerator TrackMoving(NodeClass newNode, BoulderController controller)
    {
        Vector3 goal = newNode.worldPos;
        Vector3 start = transform.position;

        float duration = TurnManager.instance.pushDuration;
        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(start, goal, t);
            yield return new WaitForFixedUpdate();
        }
        if(t > 1)
        {
            NewState(State.Idle, controller);
            transform.position = goal;
        }
    }
}
