using UnityEngine;
using UnityEngine.Events;

public class SignalTrigger : MonoBehaviour
{
    public UnityEvent OnSignal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            OnSignal?.Invoke();
        }
    }
}
