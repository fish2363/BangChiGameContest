using UnityEngine;

public class Vampire_DeadState : EnemyState
{
    public Vampire_DeadState(Enemy enemy) : base(enemy, EnemyStateType.Dead.ToString())
    {
    }

    protected override void EnterState()
    {
        base.EnterState();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
    }
}
