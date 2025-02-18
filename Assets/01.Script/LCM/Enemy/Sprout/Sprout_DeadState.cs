using UnityEngine;

public class Sprout_DeadState : EnemyState
{
    public Sprout_DeadState(Enemy enemy) : base(enemy, EnemyStateType.Dead.ToString())
    {
    }
    
    protected override void EnterState()
    {
        base.EnterState();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
    }
}
