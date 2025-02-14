using UnityEngine;

public class TextEventTrigger : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO errorChannel;

    public string errorText;
    public TextType textType;
    public bool isDefunct;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TextEvent events = UIEvent.ErrorTextEvect;
            events.Text = errorText;
            events.textType = textType;
            events.isDefunct = isDefunct;

            errorChannel.RaiseEvent(events);
            gameObject.SetActive(false);
        }
    }
}
