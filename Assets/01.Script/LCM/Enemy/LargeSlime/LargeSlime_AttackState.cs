using UnityEngine;

public class LargeSlime_AttackState : EnemyState
{
    private float mass;
    public LargeSlime_AttackState(Enemy enemy) : base(enemy, EnemyStateType.Attack.ToString())
    {
    }

    protected override void EnterState()
    {
        base.EnterState();
        mass = _enemy.RbCompo.mass;
        _enemy.RbCompo.mass = 100;
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
        _enemy.RbCompo.mass = mass;
        _enemy.isAttackAnimationEnd = false;
    }
}
