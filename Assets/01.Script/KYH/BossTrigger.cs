using UnityEngine;
using UnityEngine.Playables;

public class BossTrigger : MonoBehaviour
{
    public GameObject Boss;
    public PlayableDirector director;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            director.Play();
            collision.GetComponent<Player>().GetCompo<EntityMover>().CanManualMove = false;
        }
    }

    public void StartBoss()
    {
        Boss.SetActive(true);
        FindAnyObjectByType<Player>().GetCompo<EntityMover>().CanManualMove = true;
    }
}
