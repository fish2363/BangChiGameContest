using UnityEngine;

public class Knight_ShieldState : EnemyState
{
    public Knight_ShieldState(Enemy enemy) : base(enemy, EnemyStateType.Shield.ToString())
    {
    }

    protected override void EnterState()
    {
        base.EnterState();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
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
    }
}