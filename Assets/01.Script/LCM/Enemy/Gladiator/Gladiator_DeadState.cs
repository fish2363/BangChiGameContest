using UnityEngine;

public class Gladiator_DeadState : EnemyState
{
    public Gladiator_DeadState(Enemy enemy) : base(enemy, EnemyStateType.Dead.ToString())
    {
    }
    protected override void EnterState()
    {
        base.EnterState();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
    }
}
