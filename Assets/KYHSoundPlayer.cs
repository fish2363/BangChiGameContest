using UnityEngine;

public class KYHSoundPlayer : MonoBehaviour//�����̰� SoundPlayer �������� ��� ������ ��Ծ��뼭 ���� ����
{
    public string bgmName;
    public SoundType soundType;
    public bool isLoop;
    private void OnTriggerExit2D(Collider2D collision)
    {
        AudioManager.Instance.PlaySound2D(bgmName,0,isLoop,soundType);
    }

    public void PlaySound()
    {
        AudioManager.Instance.PlaySound2D(bgmName, 0, isLoop, soundType);
    }
}
