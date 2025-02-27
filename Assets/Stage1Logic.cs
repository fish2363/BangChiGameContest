using UnityEngine;
using Unity.Cinemachine;

public class Stage1Logic : MonoBehaviour
{
    public Player _player;
    public GameObject stage1Dialogue;
    [SerializeField]
    private GameEventChannelSO cameraChannel;
    [SerializeField] private CinemachineCamera forceChangeCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (StageChecker.Instance.GetSaveData())
        {
            SwapCameraEvent swapEvt = CameraEvents.SwapCameraEvent;
            swapEvt.isForceSwap = true;
            swapEvt.rightCamera = forceChangeCamera;
            cameraChannel.RaiseEvent(swapEvt);

            _player.isBannedAttack = false;
            _player.isLockedWindow = false;
            _player.transform.position = gameObject.transform.position;
            stage1Dialogue.SetActive(false);
        }
        else
            AudioManager.Instance.PlaySound2D("Stage1StartBGM", 0,true,SoundType.BGM);
    }
}
