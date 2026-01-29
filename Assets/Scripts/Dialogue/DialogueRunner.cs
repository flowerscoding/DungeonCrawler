using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueRunner : MonoBehaviour
{
    public Canvas dialogueCanvas;
    public TextMeshProUGUI tMeshDialogue;
    public TextMeshProUGUI tMeshSpeakerName;
    public enum State
    {
        Disabled,
        Enabled,
    }
    public State state;

    public void StateChange(State newState)
    {
        state = newState;
        RunState();
    }
    void RunState()
    {

        switch (state)
        {
            case State.Disabled:
                dialogueCanvas.enabled = false;
                break;
            case State.Enabled:
                dialogueCanvas.enabled = true;
                break;
        }
    }

    Coroutine _activeCoroutine;
    public void RunDialogue(string dialogue, string speakerName)
    {
        if (_activeCoroutine != null)
            StopCoroutine(_activeCoroutine);
        
        tMeshSpeakerName.text = speakerName;
        _activeCoroutine = StartCoroutine(DialogueCoroutine(dialogue));
    }
    IEnumerator DialogueCoroutine(string dialogue)
    {
        float cooldown = 0.1f;
        int dialogueLength = dialogue.Length;
        tMeshDialogue.text = "";
        while(tMeshDialogue.text.Length < dialogueLength)
        {
            tMeshDialogue.text += dialogue[0];
            dialogue = dialogue.Substring(1);
            yield return new WaitForSeconds(cooldown);
        }
        _activeCoroutine = null;
    }
}
