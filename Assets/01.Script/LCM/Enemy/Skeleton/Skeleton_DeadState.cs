using UnityEngine;

public class Skeleton_DeadState : EnemyState
{
    public Skeleton_DeadState(Enemy enemy) : base(enemy, EnemyStateType.Dead.ToString())
    {
    }
    
    protected override void EnterState()
    {
        base.EnterState();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
    }
}
