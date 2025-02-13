using System;
using UnityEngine;
using UnityEngine.Events;

public class Knight : Enemy
{
    [SerializeField] private float _jumpPower;
    
    [SerializeField] private EntityHealth _entityHealth;
    private bool _isPageTwo;

    protected override void Awake()
    {
        base.Awake();
        foreach (EnemyStateType stateType in Enum.GetValues(typeof(EnemyStateType)))
        {
            try
            {
                string enumName = stateType.ToString();
                Type t = Type.GetType($"Knight_{enumName}State");
                EnemyState state = Activator.CreateInstance(t, new object[] { this }) as EnemyState;
                StateEnum.Add(stateType, state);
            }
            catch
            {
                // ignore
            }
        }

        _entityHealth.hp.OnValueChanged += HandleHpDown;
        TransitionState(EnemyStateType.Idle);
    }
    

    private void HandleHpDown(float prev, float next)
    {
        if (_entityHealth.maxHealth / 2 <= next)
        {
            Debug.Log("2페이즈 진입");
        }
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

    public override void Attack()
    {
        
    }

    public override void Attakc2()
    {
        RbCompo.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
    }

    public override void RandomAttack()
    {
        if (!_isPageTwo)
        {
            int rand = UnityEngine.Random.Range(0, 2);
            if(rand == 0)
                TransitionState(EnemyStateType.Attack);
            else
                TransitionState(EnemyStateType.Attack2);
        }
        else
        {
            
        }
    }
    public override void Dead()
    {
        if (IsDead) return;
        gameObject.layer = DeadBodyLayer;
        IsDead = true;
        TransitionState(EnemyStateType.Dead);
    }
}
