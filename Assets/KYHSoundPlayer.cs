using UnityEngine;

public class KYHSoundPlayer : MonoBehaviour//찬민이가 SoundPlayer 만들어놓고 어떻게 쓰는지 까먹었대서 직접 만듦
{
    public string bgmName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.Instance.PlaySound2D(bgmName,0,true,SoundType.BGM);
    }

    public void PlaySound()
    {
        AudioManager.Instance.PlaySound2D(bgmName, 0, true, SoundType.BGM);
    }
}
