using UnityEngine;

public class Knight_PageTwoState : EnemyState
{
    private float mass;
    public Knight_PageTwoState(Enemy enemy) : base(enemy, EnemyStateType.PageTwo.ToString())
    {
    }

    protected override void EnterState()
    {
        base.EnterState();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
        _enemy.EntityHealth.IsInvincibility = true;
        mass = _enemy.RbCompo.mass;
        _enemy.RbCompo.mass = 100;
    }
    
    protected override void ExtiState()
    {
        base.ExtiState();
        _enemy.CreateShield();
        _enemy.EntityHealth.IsInvincibility = false;
        _enemy.RbCompo.mass = mass;
    }
}
