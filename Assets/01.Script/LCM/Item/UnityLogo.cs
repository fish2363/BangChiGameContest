using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class UnityLogo : MonoBehaviour, ITakeable
{
    [SerializeField] private PlayableDirector _cutScene;

    [SerializeField] private float _playerCheckRadius;

    [SerializeField] private LayerMask _whatIsPlayer;

    [SerializeField] private GameObject _interactionKey;

    private float _firstItemYPosition;

    [SerializeField] private float _offsetItemYPosition;

    [SerializeField] private float _moveTime;

    private Tweener _tweener;

    private bool _isAlreadyTake = false;
    private void Start()
    {
        _firstItemYPosition = transform.position.y;
        
        _tweener = transform.DOMoveY(_firstItemYPosition + _offsetItemYPosition, _moveTime)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }


    public void TakeItem()
    {
        _isAlreadyTake = true;
        _tweener.Kill();
        Debug.Log("누름");
        //_cutScene.Play();
    }

    public void ShowInteraction()
    {
        if (CanTakeItem())
        {
            _interactionKey.SetActive(true);
        }
        else
        {
            _interactionKey.SetActive(false);
        }
    }

    private bool CanTakeItem()
    {
        return Physics2D.OverlapCircle(transform.position, _playerCheckRadius, _whatIsPlayer);
    }

    private void Update()
    {
        if (CanTakeItem() && Keyboard.current.fKey.wasPressedThisFrame)
        {
            if(_isAlreadyTake == false)
                TakeItem();
        }
        ShowInteraction();
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _playerCheckRadius);
    }

#endif
}