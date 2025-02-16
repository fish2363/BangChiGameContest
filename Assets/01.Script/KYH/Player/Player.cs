using System;
using UnityEngine;

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
    private void OnDestroy()
    {
        GetCompo<EntityAnimationTrigger>().OnAnimationEnd -= HandleAnimationEnd;
        GetCompo<EntityHealth>().OnKnockback -= HandleKnockBack;
        PlayerInput.ClearSubscription();
    }

    public void DecreaseJumpCount() => _currentJumpCount--;
    public void ResetJumpCount() => _currentJumpCount = MaxJumpCount;

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        _mover = GetCompo<EntityMover>();
        GetCompo<EntityHealth>().OnKnockback += HandleKnockBack;
        GetCompo<EntityAnimationTrigger>().OnAnimationEnd += HandleAnimationEnd;
        _currentJumpCount = MaxJumpCount;
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
}
