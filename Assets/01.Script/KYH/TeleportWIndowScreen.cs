using System;
using System.Collections;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

public class TeleportWIndowScreen : MonoBehaviour,IEntityComponent,IAfterInit
{
    
    [field:SerializeField] public Transform windowScreenTrans;
    [SerializeField] private Vector2 enterKnockbackForce;


    private Player _player;
    private Transform prevTrans;
    private EntityMover _mover;

    private CinemachineCamera prevCamera;
    public CinemachineCamera windowScreenCamera;

    [SerializeField] private int activeCameraPriority = 15;
    [SerializeField] private int disableCameraPriority = 10;

    [SerializeField] private GameEventChannelSO cameraChannel;

    private bool isEnterWindow;

    public void Initialize(Entity entity)
    {
        _player = entity as Player;
        _mover = entity.GetCompo<EntityMover>();
    }

    public void AfterInitialize()
    {
        _player.PlayerInput.OnEnterWindowKeyPressed += HandleEnterWindowScreen;
    }

    private IEnumerator WindowEffect()
    {
        _mover.CanManualMove = false;
        _mover.EffectorPlayer.PlayEffect("ReadyEnterWindow",false);
        yield return new WaitForSeconds(2f);
        _mover.EffectorPlayer.PlayEffect("EnterWindow", true);
        yield return new WaitForSeconds(0.25f);

        _mover.EffectorPlayer.StopEffect("ReadyEnterWindow");
        isEnterWindow = true;
        prevTrans = _player.transform;
        _player.transform.position = windowScreenTrans.position;
        _mover.KnockBack(enterKnockbackForce, 0.5f);

        Vector2 exitDirection = Vector2.right;

        prevCamera = FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None)
                        .FirstOrDefault(cam => cam.Priority == activeCameraPriority);

        SwapCameraEvent swapEvt = CameraEvents.SwapCameraEvent;
        swapEvt.leftCamera = prevCamera;
        swapEvt.rightCamera = windowScreenCamera;
        swapEvt.moveDirection = exitDirection;
        swapEvt.isBattonFollow = false;
        _mover.CanManualMove = true;

        cameraChannel.RaiseEvent(swapEvt);
    }

    private void OnDestroy()
    {
        _player.PlayerInput.OnEnterWindowKeyPressed -= HandleEnterWindowScreen;
    }

    private void HandleEnterWindowScreen()
    {
        if (_player.isLockedWindow|| isEnterWindow) return;

        StartCoroutine(WindowEffect());
    }

    public void ComebackPrevTrans()
    {
        Vector2 exitDirection = Vector2.right;

        SwapCameraEvent swapEvt = CameraEvents.SwapCameraEvent;
        swapEvt.leftCamera = windowScreenCamera;
        swapEvt.rightCamera = prevCamera;
        swapEvt.moveDirection = exitDirection;
        swapEvt.isBattonFollow = false;

        cameraChannel.RaiseEvent(swapEvt);
    }
}
