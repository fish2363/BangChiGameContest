using UnityEngine;

public class DrawSlime_AttackState : EnemyState
{
    public DrawSlime_AttackState(Enemy enemy) : base(enemy, EnemyStateType.Attack.ToString())
    {
        
    }

    protected override void EnterState()
    {
        base.EnterState();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
        _enemy.Attack();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(_enemy.isAttackAnimationEnd)
            _enemy.TransitionState(EnemyStateType.Move);
    }

    protected override void ExtiState()
    {
        base.ExtiState();
        _enemy.isAttackAnimationEnd = false;
    }
}
