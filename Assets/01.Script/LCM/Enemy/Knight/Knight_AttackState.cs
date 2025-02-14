using UnityEngine;

public class Knight_AttackState : EnemyState
{
    public Knight_AttackState(Enemy enemy) : base(enemy, EnemyStateType.Attack.ToString())
    {
    }
    
    protected override void EnterState()
    {
        base.EnterState();
        _enemy.Attack();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
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
