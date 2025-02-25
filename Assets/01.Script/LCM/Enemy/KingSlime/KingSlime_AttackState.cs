using UnityEngine;

public class KingSlime_AttackState : EnemyState
{
    private float mass;
    public KingSlime_AttackState(Enemy enemy) : base(enemy, EnemyStateType.Attack.ToString())
    {
    }
    
    protected override void EnterState()
    {
        base.EnterState();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
        mass = _enemy.RbCompo.mass;
        _enemy.RbCompo.mass = 100;
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
        _enemy.RbCompo.mass = mass;
    }
}
