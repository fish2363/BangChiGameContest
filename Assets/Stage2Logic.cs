using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2Logic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlaySound2D("Stage2MainBGM", 0, true, SoundType.BGM);
    }

    public void Restart()
    {
        AudioManager.Instance.StopAllLoopSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }
}
