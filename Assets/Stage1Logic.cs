using UnityEngine;

public class Stage1Logic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlaySound2D("Stage1StartBGM", 0,true,SoundType.BGM);
    }
}
