using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StageChecker.Instance.SavePoint();
            gameObject.SetActive(false);
        }
    }
}
