using System;
using UnityEngine;
using UnityEngine.Events;


public enum QueenSlimeBuffType
{
    Attack,
    Defend,
    Heal
}
public class QueenSlime : Enemy
{
    public UnityEvent<QueenSlimeBuffType> OnBuff;
    private int _rand;
    protected override void Awake()
    {
        base.Awake();
        foreach (EnemyStateType stateType in Enum.GetValues(typeof(EnemyStateType)))
        {
            try
            {
                string enumName = stateType.ToString();
                Type t = Type.GetType($"QueenSlime_{enumName}State");
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

    public override void RandomAttack()
    {
        _rand = UnityEngine.Random.Range(0, 3);
        if(_rand == 0)
            OnBuff?.Invoke(QueenSlimeBuffType.Attack);
        else if(_rand == 1)
            OnBuff?.Invoke(QueenSlimeBuffType.Defend);
        else
            OnBuff?.Invoke(QueenSlimeBuffType.Heal);
        
    }

    public override void Attack()
    {
        
    }

    public override void Dead()
    {
        if (IsDead) return;
        gameObject.layer = DeadBodyLayer;
        IsDead = true;
        TransitionState(EnemyStateType.Dead);
    }
}
