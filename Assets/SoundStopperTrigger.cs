using UnityEngine;

public class SoundStopperTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            AudioManager.Instance.StopLoopSound(AudioManager.Instance.CurrentMainBGMName);
    }

    public void StopSound()
    {
        AudioManager.Instance.StopLoopSound(AudioManager.Instance.CurrentMainBGMName);
    }

    public void StopNameSound(string songName)
    {
        AudioManager.Instance.StopLoopSound(songName);
    }

}
