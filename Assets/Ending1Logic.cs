using UnityEngine;

public class Ending1Logic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.StopAllLoopSound();
        AudioManager.Instance.PlaySound2D("Ending1",0,true,SoundType.BGM);
    }
}
