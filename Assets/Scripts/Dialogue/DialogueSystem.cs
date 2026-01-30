using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;
    public DialogueRunner dialogueRunner;
    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void RunDialogue()
    {
        NpcController controller = Player.instance.playerInteract.curInteractingNode.npcController;
        DialogueTracker tracker = controller.dialogueTracker;
        DialogueLines lines = controller.dialogueLines;

        if(tracker.index >= lines.lines.Length)
        {
            dialogueRunner.StateChange(DialogueRunner.State.Disabled);
            tracker.indexChange(0);

            Player.instance.StateChange(PlayerState.State.Dead);
            return;
        }

        dialogueRunner.StateChange(DialogueRunner.State.Enabled);
        dialogueRunner.RunDialogue(lines.lines[tracker.index], lines.speakerName);
        tracker.indexChange(tracker.index + 1);
    }
}
