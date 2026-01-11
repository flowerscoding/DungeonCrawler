using UnityEngine;

public class BoulderController : MonoBehaviour
{
    public BoulderState boulderState;
    public  GridAgent gridAgent;
    public void NewState(BoulderState.State newState)
    {
        boulderState.NewState(newState, this);
    }
    public void ResetBoulder(NodeClass originNode)
    {
        
        gridAgent.SetNode(originNode);
        NewState(BoulderState.State.Idle);
    }
}
