using UnityEngine;

public class DialogueLines : MonoBehaviour
{
    public string speakerName;
    public string[] lines;
    public string RecieveDialogue(int lineNumber)
    {
        return lines[lineNumber];
    } 
}
