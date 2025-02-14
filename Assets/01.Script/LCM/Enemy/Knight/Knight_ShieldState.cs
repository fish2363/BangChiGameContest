using UnityEngine;

public class Knight_ShieldState : EnemyState
{
    private float mass;
    public Knight_ShieldState(Enemy enemy) : base(enemy, EnemyStateType.Shield.ToString())
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

    public override void UpdateState()
    {
        base.UpdateState();
        if (_enemy.isAttackAnimationEnd)
            _enemy.TransitionState(EnemyStateType.Move);
    }

    protected override void ExtiState()
    {
        base.ExtiState();
        _enemy.CreateShield();
        _enemy.isAttackAnimationEnd = false;
        _enemy.EntityHealth.IsInvincibility = false;
        _enemy.RbCompo.mass = mass;
    }
}