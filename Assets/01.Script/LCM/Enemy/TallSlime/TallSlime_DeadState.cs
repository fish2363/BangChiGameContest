using UnityEngine;

public class TallSlime_DeadState : EnemyState
{
    public TallSlime_DeadState(Enemy enemy) : base(enemy, EnemyStateType.Dead.ToString())
    {
    }

    protected override void EnterState()
    {
        base.EnterState();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
    }
}
