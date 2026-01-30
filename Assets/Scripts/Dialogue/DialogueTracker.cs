using UnityEngine;

public class DialogueTracker : MonoBehaviour
{
    public int index {get; private set;}
    public void indexChange(int newIndex)
    {
        index = newIndex;
    }
}
