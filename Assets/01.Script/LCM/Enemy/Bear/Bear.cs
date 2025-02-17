using System;
using UnityEngine;

public class Bear : Enemy, ICounterable
{
    private EntityAnimationTrigger _animationTrigger;
    protected override void Awake()
    {
        base.Awake();
        foreach (EnemyStateType stateType in Enum.GetValues(typeof(EnemyStateType)))
        {
            try
            {
                string enumName = stateType.ToString();
                Type t = Type.GetType($"Bear_{enumName}State");
                EnemyState state = Activator.CreateInstance(t, new object[] { this }) as EnemyState;
                StateEnum.Add(stateType, state);
            }
            catch
            {
                // ignore
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
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GetCompo<EntityHealth>().OnKnockback -= HandleKnockBack;
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
        
    }

    public override void Attakc2()
    {
        
    }

    public override void RandomAttack()
    {
        int rand = UnityEngine.Random.Range(0, 2);
        if(rand == 0)
            TransitionState(EnemyStateType.Attack);
        else
            TransitionState(EnemyStateType.Attack2);
    }

    public override void Dead()
    {
        if (IsDead) return;
        gameObject.layer = DeadBodyLayer;
        IsDead = true;
        TransitionState(EnemyStateType.Dead);
    }
    #region Counter section
    public bool CanCounter { get; private set; }
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
