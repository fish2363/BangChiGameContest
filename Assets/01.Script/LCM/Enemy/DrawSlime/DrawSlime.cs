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
            catch
            {
                // ignored
            }
        }
        TransitionState(EnemyStateType.Idle);
    }

    protected override void HandleHit()
    {
    }

    protected override void HandleDead()
    {
        Dead();
    }

    public override void Attack()
    {
        RbCompo.AddForce(GetMovementDirection().normalized * _attackDashPower, ForceMode2D.Impulse);
    }

    public override void Dead()
    {
        TransitionState(EnemyStateType.Dead);
    }
}
