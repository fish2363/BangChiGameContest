using UnityEngine;

public static class DialogueEvents
{
    public static StartAConversation StartAConversation = new();
}

public class StartAConversation : GameEvent
{
    public string[] dialogue;
    public bool isStop;
}
