using UnityEngine;

public class KYHSoundPlayer : MonoBehaviour//�����̰� SoundPlayer �������� ��� ������ ��Ծ��뼭 ���� ����
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
