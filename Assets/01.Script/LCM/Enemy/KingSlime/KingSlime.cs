using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KingSlime : Enemy, ICounterable
{
    private EntityAnimationTrigger _animationTrigger;

    [SerializeField] private float _jumpPower;

    public UnityEvent OnAttack3;

    public List<EnemyAttackStruct> _kingSlimeAttacks;
    
    private int _attackIndex;
    private EnemyAttackCompo _enemyAttackCompo;

    protected override void Awake()
    {
        base.Awake();
        foreach (EnemyStateType stateType in Enum.GetValues(typeof(EnemyStateType)))
        {
            try
            {
                string enumName = stateType.ToString();
                Type t = Type.GetType($"KingSlime_{enumName}State");
                EnemyState state = Activator.CreateInstance(t, new object[] { this }) as EnemyState;
                StateEnum.Add(stateType, state);
            }
            catch
            {
                // ignore
            }
        }

        _enemyAttackCompo = GetComponentInChildren<EnemyAttackCompo>();
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
        float knockBackTime = 0.5f;
        KnockBack(knockBackForce, knockBackTime);
    }

    protected override void HandleHit()
    {
    }

    protected override void HandleDead() => Dead();

    public override void Attack()
    {
        _attackIndex = 0;
        _enemyAttackCompo.AttackSetting(_kingSlimeAttacks[_attackIndex].damage, _kingSlimeAttacks[_attackIndex].force,
            _kingSlimeAttacks[_attackIndex].attackBoxSize, _kingSlimeAttacks[_attackIndex].attackRadius, _kingSlimeAttacks[_attackIndex].castType);
    }

    public override void Attakc2()
    {
        _attackIndex = 1;
        _enemyAttackCompo.AttackSetting(_kingSlimeAttacks[_attackIndex].damage, _kingSlimeAttacks[_attackIndex].force,
            _kingSlimeAttacks[_attackIndex].attackBoxSize, _kingSlimeAttacks[_attackIndex].attackRadius, _kingSlimeAttacks[_attackIndex].castType);
    }

    public override void Attakc3()
    {
        _attackIndex = 2;
        _enemyAttackCompo.AttackSetting(_kingSlimeAttacks[_attackIndex].damage, _kingSlimeAttacks[_attackIndex].force,
            _kingSlimeAttacks[_attackIndex].attackBoxSize, _kingSlimeAttacks[_attackIndex].attackRadius, _kingSlimeAttacks[_attackIndex].castType);
        StartCoroutine(Attack3Cor());
    }

    private IEnumerator Attack3Cor()
    {
        Vector2 dir = GetMovementDirection().normalized;
        RbCompo.AddForce(new Vector2(dir.x, 1f) * _jumpPower, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        OnAttack3?.Invoke();
    }

    public override void RandomAttack()
    {
        int rand = UnityEngine.Random.Range(0, 3);
        if (rand == 0)
            TransitionState(EnemyStateType.Attack);
        else if (rand == 1)
            TransitionState(EnemyStateType.Attack2);
        else
            TransitionState(EnemyStateType.Attack3);
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
        float stunTime = 2f;

        CanCounter = false;

        GetCompo<EntityHealth>().ApplyDamage(damage, direction, knockBackForce, isPowerAttack, dealer);
        Debug.Log("<color=green>Counter success</color>");
    }

    private void SetCounterStatus(bool canCounter)
        => CanCounter = canCounter;

    #endregion
}