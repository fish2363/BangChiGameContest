using UnityEngine;

public class Marksman_AttackState : EnemyState
{
    public Marksman_AttackState(Enemy enemy) : base(enemy, EnemyStateType.Attack.ToString())
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
        _enemy.EnemyRotation();
        if(_enemy.isAttackAnimationEnd)
            _enemy.TransitionState(EnemyStateType.Move);
    }

    protected override void ExtiState()
    {
        base.ExtiState();
        _enemy.isAttackAnimationEnd = false;
    }
}
