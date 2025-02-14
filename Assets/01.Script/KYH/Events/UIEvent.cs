using UnityEngine;

public enum TextType
{
    Help,
    Error
}

public static class UIEvent
{
    public static TextEvent ErrorTextEvect = new();
}

public class TextEvent  : GameEvent
{
    public string Text;
    public bool isDefunct;
    public TextType textType;
}