using UnityEngine;

public class QueenSlime_DeadState : EnemyState
{
    public QueenSlime_DeadState(Enemy enemy) : base(enemy, EnemyStateType.Dead.ToString())
    {
    }
    
    protected override void EnterState()
    {
        base.EnterState();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
    }
}
