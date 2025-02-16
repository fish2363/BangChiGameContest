using UnityEngine;

public class TalkToEachOtherDialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO dialogueChannel;
    public string[] dialogue;
    public NpcDialogueComponent talker;
    public bool IsStartTalkerPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dialogue.Length < 1) return;

        if (collision.CompareTag("Player"))
        {
            StartATalkEachOther events = DialogueEvents.StartATalkEachOther;
            events.talker = talker;
            events.dialogue = dialogue;
            events.startTalkerIsPlayer = IsStartTalkerPlayer;

            dialogueChannel.RaiseEvent(events);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
