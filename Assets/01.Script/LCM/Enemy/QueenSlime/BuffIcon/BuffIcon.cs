using System;
using UnityEngine;

public class BuffIcon : Enemy
{
    [SerializeField] private Transform _queen;
    protected override void Awake()
    {
        base.Awake();
        foreach (EnemyStateType stateType in Enum.GetValues(typeof(EnemyStateType)))
        {
            try
            {
                string enumName = stateType.ToString();
                Type t = Type.GetType($"BuffIcon_{enumName}State");
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
    protected override void HandleHit()
    {
        
    }

    protected override void HandleDead()
    {
    }

    public override void Attack()
    {
    }

    public override void Dead()
    {
    }

    public void BuffIconChange(QueenSlimeBuffType buffType)
    {
        transform.position = new Vector3(_queen.transform.position.x, transform.position.y);
        if (buffType == QueenSlimeBuffType.Attack)
        {
            TransitionState(EnemyStateType.Attack);
        }
        else if (buffType == QueenSlimeBuffType.Defend)
        {
            TransitionState(EnemyStateType.Attack2);
        }
        else
        {
            TransitionState(EnemyStateType.Attack3);
        }
    }
}
