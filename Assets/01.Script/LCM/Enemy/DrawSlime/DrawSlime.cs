using System;
using UnityEngine;

public class DrawSlime : Enemy
{
    [SerializeField] private float _attackDashPower;
    public override void Awake()
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
            catch (Exception e)
            {
                // ignored
            }
        }
        TransitionState(EnemyStateType.Idle);
    }

    public override void Attack()
    {
        Debug.Log("공격");
        RbCompo.AddForce(GetMovementDirection().normalized * _attackDashPower, ForceMode2D.Impulse);
    }

    public override void Dead()
    {
        OnDeadEvent?.Invoke();
        TransitionState(EnemyStateType.Dead);
        Debug.Log("죽음");
    }
}
