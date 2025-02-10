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

    protected override void HandleDead() => Dead();

    public override void Attack()
    {
        Vector2 movementDirection = GetMovementDirection().normalized;

        RbCompo.AddForce(movementDirection * _attackDashPower, ForceMode2D.Impulse);
        RbCompo.AddForce(Vector2.up * 2f, ForceMode2D.Impulse); // 살짝 위로 뛰도록
    }

    public override void Dead()
    {
        TransitionState(EnemyStateType.Dead);
    }
}
