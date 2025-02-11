using UnityEngine;

public class DrawSlime_DeadState : EnemyState
{
    public DrawSlime_DeadState(Enemy enemy) : base(enemy, EnemyStateType.Dead.ToString())
    {
        
    }

    protected override void EnterState()
    {
        base.EnterState();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
    }
}
