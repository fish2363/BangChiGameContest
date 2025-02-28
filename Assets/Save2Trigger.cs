using UnityEngine;

public class Save2Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Stage2Checker.Instance.SavePoint();
            gameObject.SetActive(false);
        }
    }
}
