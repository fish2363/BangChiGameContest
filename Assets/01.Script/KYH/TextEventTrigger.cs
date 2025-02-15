using UnityEngine;

public class TextEventTrigger : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO textChannel;

    public string errorText;
    public TextType textType;
    public KeyCode keySkipType;
    public bool isDefunct;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TextEvent events = UIEvent.ErrorTextEvect;
            events.Text = errorText;
            events.textType = textType;
            events.isDefunct = isDefunct;
            events.TextSkipKey = keySkipType;

            textChannel.RaiseEvent(events);
            gameObject.SetActive(false);
        }
    }
}
