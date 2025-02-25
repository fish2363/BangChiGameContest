using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AutoNpc : Npc
{
    
    [SerializeField] private bool _randomChat;
    [SerializeField] private Rigidbody2D RbCompo;

    [SerializeField] private Animator _animator;
    
    private bool _intoRangePlayer;

    //좋은 구조는 아니지만 편하게 만들기 위해
    [SerializeField]
    private string animationName;

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        GetCompo<EntityHealth>().OnKnockback += HandleKnockBack;
    }

    private void HandleKnockBack(Vector2 knockBackForce)
    {
        float knockBackTime = 0.5f;
        KnockBack(knockBackForce, knockBackTime);
    }

    public void AddForceToEntity(Vector2 force)
            => RbCompo.AddForce(force, ForceMode2D.Impulse);
    public void StopImmediately(bool isYAxisToo)
    {
        if (isYAxisToo)
            RbCompo.linearVelocity = Vector2.zero;
        else
            RbCompo.linearVelocityX = 0;
    }

    private void KnockBack(Vector2 knockBackForce, float knockBackTime)
    {
        StopImmediately(true);
        AddForceToEntity(new Vector2(0, knockBackForce.y));
    }


    protected override void HandleDead()
    {
    }

    protected override void HandleHit()
    {
        _animator.Play(animationName);
    }

    private IEnumerator ChattingCoroutine()
    {
        _isTalking = true;
        if(_randomChat)
            _nowText = NpcChatTexts[UnityEngine.Random.Range(0, NpcChatTexts.Count)];
        else
        {
            if (_npcIndex >= NpcChatTexts.Count)
            {
                _npcIndex = 0;
                _nowText = NpcChatTexts[_npcIndex];
                _npcIndex++;
            }
            else
            {
                _nowText = NpcChatTexts[_npcIndex];
                _npcIndex++;
            }
        }
        // 여기까지가 말할 text정하는거

        _textBox.SetActive(true);
        
        yield return _textBoxText.DOText(_nowText, _nowText.Length / 5f).SetEase(Ease.OutCubic).WaitForCompletion();
        
        yield return new WaitForSeconds(_waitTime);

        _textBoxText.text = "";
        _textBox.SetActive(false);

        yield return new WaitForSeconds(_waitTime);
        
        _isTalking = false;
        
        if(_intoRangePlayer)
            StartCoroutine(ChattingCoroutine());
        else
        {
            StopAllCoroutines();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        StopAllCoroutines();
    }

    private void Update()
    {
        if (!_intoRangePlayer && !_isTalking && Physics2D.OverlapCircle(transform.position, _chatRadius, _whatIsPlayer))
        {
            _intoRangePlayer = true;
            StartCoroutine(ChattingCoroutine());
            Debug.Log("코루틴 시작");
        }
        else if(!Physics2D.OverlapCircle(transform.position, _chatRadius, _whatIsPlayer))
        {
            _intoRangePlayer = false;
        }
    }
}
