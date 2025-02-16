using UnityEngine;
using UnityEngine.Playables;

public class TalkToEachOtherDialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO dialogueChannel;

    public string[] dialogue;
    public NpcDialogueComponent talker;
    public bool IsStartTalkerPlayer;

    public float distance;
    public float panTime;
    public PanDirection direction;

    [SerializeField] private PlayableDirector[] director;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dialogue.Length < 1) return;

        if (collision.CompareTag("Player"))
        {
            StartATalkEachOther events = DialogueEvents.StartATalkEachOther;
            events.talker = talker;
            events.dialogue = dialogue;
            events.startTalkerIsPlayer = IsStartTalkerPlayer;
            events.panTime = panTime;
            events.npcDistance = distance;
            events.npcDirection = direction;
            events.director = director;

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
