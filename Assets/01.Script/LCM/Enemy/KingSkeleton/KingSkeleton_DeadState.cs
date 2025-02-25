using UnityEngine;

public class KingSkeleton_DeadState : EnemyState
{
    public KingSkeleton_DeadState(Enemy enemy) : base(enemy, EnemyStateType.Dead.ToString())
    {
    }

    protected override void EnterState()
    {
        base.EnterState();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
    }
}
