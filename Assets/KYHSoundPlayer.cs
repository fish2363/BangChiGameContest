using UnityEngine;

public class KYHSoundPlayer : MonoBehaviour//�����̰� SoundPlayer �������� ��� ������ ��Ծ��뼭 ���� ����
{
    public string bgmName;
    public SoundType soundType;
    public bool isLoop;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.Instance.PlaySound2D(bgmName,0,isLoop,soundType);
    }

    public void PlaySound()
    {
        print("dddddddddddddddddddddddddd");
        AudioManager.Instance.PlaySound2D(bgmName, 0, isLoop, soundType);
    }
}
