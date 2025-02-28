using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2Logic : MonoBehaviour
{
    public Player _player;

    [SerializeField]
    private GameEventChannelSO cameraChannel;
    [SerializeField] private CinemachineCamera forceChangeCamera;
    [SerializeField] private GameObject esterEgg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Stage2Checker.Instance.GetSaveData())
        {
            SwapCameraEvent swapEvt = CameraEvents.SwapCameraEvent;
            swapEvt.isForceSwap = true;
            swapEvt.rightCamera = forceChangeCamera;
            cameraChannel.RaiseEvent(swapEvt);
            AudioManager.Instance.PlaySound2D("Dark 4", 0, true, SoundType.BGM);
            _player.isBannedAttack = false;
            esterEgg.SetActive(true);
            _player.transform.position = gameObject.transform.position;
        }
        else
            AudioManager.Instance.PlaySound2D("Stage2MainBGM", 0, true, SoundType.BGM);
    }

    public void Restart()
    {
        AudioManager.Instance.StopAllLoopSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }
}
