using UnityEngine;

public class LargeSlime_DeadState : EnemyState
{
    public LargeSlime_DeadState(Enemy enemy) : base(enemy, EnemyStateType.Dead.ToString())
    {
    }

    protected override void EnterState()
    {
        base.EnterState();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
    }
}
