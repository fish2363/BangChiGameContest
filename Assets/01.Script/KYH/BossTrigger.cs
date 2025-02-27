using UnityEngine;
using UnityEngine.Playables;

public class BossTrigger : MonoBehaviour
{
    public GameObject Boss;
    public PlayableDirector director;
    public string SongName;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(director!=null)
            director.Play();
            collision.GetComponent<Player>().GetCompo<EntityMover>().CanManualMove = false;
        }
    }

    public void StartBoss()
    {
        Boss.SetActive(true);
        AudioManager.Instance.PlaySound2D("SongName",0,true,SoundType.BGM);
        FindAnyObjectByType<Player>().GetCompo<EntityMover>().CanManualMove = true;
    }
}
