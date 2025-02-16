using UnityEngine;

public static class DialogueEvents
{
    public static StartAConversation StartAConversation = new();
    public static StartATalkEachOther StartATalkEachOther = new();
}

public class StartAConversation : GameEvent
{
    public string[] dialogue;
    public bool isStop;
}

public class StartATalkEachOther : GameEvent
{
    public string[] dialogue;
    public NpcDialogueComponent talker;
    public bool startTalkerIsPlayer;
}
