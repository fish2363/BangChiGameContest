using UnityEngine;

public class Wolf_DeadState : EnemyState
{
    public Wolf_DeadState(Enemy enemy) : base(enemy, EnemyStateType.Dead.ToString())
    {
    }
    
    protected override void EnterState()
    {
        base.EnterState();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
    }
}
