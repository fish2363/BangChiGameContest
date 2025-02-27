using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UnityLogo : MonoBehaviour, ITakeable
{

    [SerializeField] private float _playerCheckRadius;

    [SerializeField] private LayerMask _whatIsPlayer;

    [SerializeField] private GameObject _interactionKey;

    private float _firstItemYPosition;

    [SerializeField] private float _offsetItemYPosition;

    [SerializeField] private float _moveTime;

    private Tweener _tweener;

    private bool _isAlreadyTake = false;

    [SerializeField] private GameObject _blackCircle;
    
    [SerializeField] private string _sceneName;

    public UnityEvent OnCameraShakeing;

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
        _interactionKey.SetActive(false);
        _tweener.Kill();
        StartCoroutine(ItemEffect());
    }

    private IEnumerator ItemEffect()
    {
        OnCameraShakeing?.Invoke();
        yield return new WaitForSeconds(1.5f);
        OnCameraShakeing?.Invoke();
        yield return new WaitForSeconds(1.5f);

        yield return _blackCircle.transform.DOScale(3000f, 4f).WaitForCompletion();

        yield return new WaitForSeconds(0.5f);
        
        SceneManager.LoadScene(_sceneName);
    }

    public void ShowInteraction()
    {
        if(_isAlreadyTake) return;
        
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