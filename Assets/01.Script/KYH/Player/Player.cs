using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Player : Entity
{
    [field: SerializeField] public GameEventChannelSO PlayerChannel { get; private set; }
    [field: SerializeField] public InputReader PlayerInput { get; private set; }
    private StateMachine _stateMachine;

    [field: SerializeField]
    public AnimParamSO ComboCounterParam { get; private set; }

    [SerializeField]
    private StateListSO playerFSM;
    private EntityMover _mover;

    public GameObject attackLockIcon;

    [field:SerializeField]public bool isBannedAttack { get; set; } = false;
    [field:SerializeField]public bool isLockedWindow { get; set; } = false;
    [field:SerializeField]public bool isDialogue { get; set; } = false;

    [field : SerializeField]public int MaxJumpCount { get; set; }
    private int _currentJumpCount;
    public bool CanJump => _currentJumpCount > 0;

    public int TipCount { get; set; }

    [field : SerializeField] public float MaxDashCoolTime { get; set; }

    [SerializeField] private CanvasGroup dashCanvasGroup;
    [SerializeField] private CanvasGroup hpBarCanvasGroup;



    protected override void Awake()
    {
        base.Awake();
        _stateMachine = new StateMachine(this,playerFSM);
    }
    private void Start()
    {
        _stateMachine.ChangeState("IDLE");
    }

    public void BannedAttack(bool isValue)
    {
        print("½ºÀ¾");
        isBannedAttack = isValue;
    }

    private void Update()
    {
        _stateMachine.UpdateStateMachine();
    }
    private void HandleAnimationEnd()
    {
        _stateMachine.CurrentState.AnimationEndTrigger();
    }
    public void ChangeState(string newState) => _stateMachine.ChangeState(newState);

    public bool MoveStopOrGo(bool isMove) => _mover.CanManualMove = isMove;
    protected override void OnDestroy()
    {
        GetCompo<EntityAnimationTrigger>().OnAnimationEnd -= HandleAnimationEnd;
        GetCompo<EntityHealth>().OnKnockback -= HandleKnockBack;
        PlayerInput.ClearSubscription();
        GetCompo<EntityRenderer>().OnFlip -= Flip;
    }

    public void DecreaseJumpCount() => _currentJumpCount--;
    public void ResetJumpCount() => _currentJumpCount = MaxJumpCount;

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        _mover = GetCompo<EntityMover>();
        GetCompo<EntityHealth>().OnKnockback += HandleKnockBack;
        GetCompo<EntityAnimationTrigger>().OnAnimationEnd += HandleAnimationEnd;
        GetCompo<EntityRenderer>().OnFlip += Flip;
        _currentJumpCount = MaxJumpCount;
    }
    private void Flip(bool LeftOrRight)
    {
        if (LeftOrRight)
        {
            hpBarCanvasGroup.transform.Rotate(0, 180f, 0);
        }
        else
        {
            hpBarCanvasGroup.transform.Rotate(0, 0, 0);
        }
    }
    protected override void HandleHit()
    {

    }

    private void HandleKnockBack(Vector2 knockBackForce)
    {
        float knockBackTime = 0.5f;
        _mover.KnockBack(knockBackForce, knockBackTime);
    }

    
    protected override void HandleDead()
    {
        if (IsDead) return;
        gameObject.layer = DeadBodyLayer;
        IsDead = true;
        print("²Ð µðÁü");
        _stateMachine.ChangeState("DEAD");
    }

    #region ´ë½¬ÄðÅ¸ÀÓ
    private bool isCounting;

    private float targetTime;

    [SerializeField] private Slider _dashSlider;
    [SerializeField] private Slider _backSlider;

    public void SetCountTime(int time)
    {
        MaxDashCoolTime = time;
    }
    
    public void TurnOnTimer()
    {
        dashCanvasGroup.DOFade(1,0.5f);
        isCounting = true;
    }

    private void FixedUpdate()
    {
        if(isCounting)
        {
            float diffSec = targetTime += Time.deltaTime;
            print(diffSec);
            if (diffSec < MaxDashCoolTime)
            {
                CoolTimeSecondValueChangeSlider(diffSec);
            }
            else
            {
                targetTime = 0f;
                isCounting = false;
                dashCanvasGroup.DOFade(0, 0.5f);
            }
        }
    }

    public bool IsCount()
    {
        return isCounting;
    }

    public void CoolTimeSecondValueChangeSlider(float second)
    {
        if (second <= 0f) return;

        _dashSlider.value = second;

        if (_backSlider != null && _backSlider.value > _dashSlider.value)
        {
            DOTween.Sequence()
                .AppendInterval(1f)
                .Append(_backSlider.DOValue(second, 0.5f).SetEase(Ease.OutCubic));
        }
        else if (_backSlider != null && _backSlider.value < _dashSlider.value)
        {
            _backSlider.value = second;
        }
    }
    #endregion
}
