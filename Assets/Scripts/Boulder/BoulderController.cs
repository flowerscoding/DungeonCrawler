using UnityEngine;

public class BoulderController : MonoBehaviour
{
    public BoulderState boulderState;

    public void NewState(BoulderState.State newState)
    {
        boulderState.NewState(newState, this);
    }
}
