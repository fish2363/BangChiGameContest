using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StageChecker.Instance.SavePoint();
        gameObject.SetActive(false);
    }
}
