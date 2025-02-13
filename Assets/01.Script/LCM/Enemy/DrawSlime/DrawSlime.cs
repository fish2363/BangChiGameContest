using System;
using UnityEngine;

public class DrawSlime : Enemy, ICounterable
{
    private EntityAnimationTrigger _animationTrigger;
    [SerializeField] private float _attackDashPower;

    protected override void Awake()
    {
        base.Awake();
        foreach (EnemyStateType stateType in Enum.GetValues(typeof(EnemyStateType)))
        {
            try
            {
                string enumName = stateType.ToString();
                Type t = Type.GetType($"DrawSlime_{enumName}State");
                EnemyState state = Activator.CreateInstance(t, new object[] { this }) as EnemyState;
                StateEnum.Add(stateType, state);
            }
            catch
            {
                // ignored
            }
        }
        TransitionState(EnemyStateType.Idle);
    }

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        GetCompo<EntityHealth>().OnKnockback += HandleKnockBack;
        _animationTrigger = GetCompo<EntityAnimationTrigger>();
        _animationTrigger.OnCounterStatusChange += SetCounterStatus;
        print("아");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GetCompo<EntityHealth>().OnKnockback -= HandleKnockBack;
        _animationTrigger.OnCounterStatusChange -= SetCounterStatus;
    }

    private void HandleKnockBack(Vector2 knockBackForce)
    {
        print("넉백");
        float knockBackTime = 0.5f;
        KnockBack(knockBackForce, knockBackTime);
    }

    protected override void HandleHit()
    {
        
    }

    protected override void HandleDead() => Dead();

    public override void Attack()
    {
        Vector2 movementDirection = GetMovementDirection().normalized;

        RbCompo.AddForce(movementDirection * _attackDashPower, ForceMode2D.Impulse);
        RbCompo.AddForce(Vector2.up * 2f, ForceMode2D.Impulse); // 살짝 위로 뛰도록
    }

    public override void Dead()
    {
        if (IsDead) return;
        gameObject.layer = DeadBodyLayer;
        IsDead = true;
        print("꽥 디짐");
        TransitionState(EnemyStateType.Dead);
    }

    #region Counter section
    public bool CanCounter { get; private set; }
    public Transform TargetTrm => transform;
    public void ApplyCounter(float damage, Vector2 direction, Vector2 knockBackForce, bool isPowerAttack, Entity dealer)
    {
        //damage에 스턴시간, 크리티컬 등등의 정보객체 넘어와야 하는데 지금은 damage만 주니까 하드코딩
        float stunTime = 2f;

        CanCounter = false;

        GetCompo<EntityHealth>().ApplyDamage(damage, direction, knockBackForce, isPowerAttack, dealer);
        Debug.Log("<color=green>Counter success</color>");
    }

    private void SetCounterStatus(bool canCounter)
        => CanCounter = canCounter;

    #endregion
}
