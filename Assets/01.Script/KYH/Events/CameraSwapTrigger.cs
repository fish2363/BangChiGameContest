using Unity.Cinemachine;
using UnityEngine;

public class CameraSwapTrigger : MonoBehaviour
{
    public CinemachineCamera leftCamera;
    public CinemachineCamera rightCamera;
    public bool isCameraFollowPlayer;

    [SerializeField] private GameEventChannelSO cameraChannel;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (leftCamera is null || rightCamera is null) return;

        if (other.CompareTag("Player"))
        {
            Vector2 exitDirection = (other.transform.position - transform.position).normalized;

            SwapCameraEvent swapEvt = CameraEvents.SwapCameraEvent;
            swapEvt.leftCamera = leftCamera;
            swapEvt.rightCamera = rightCamera;
            swapEvt.moveDirection = exitDirection;
            swapEvt.isBattonFollow = isCameraFollowPlayer;

            cameraChannel.RaiseEvent(swapEvt);
        }
    }
}
