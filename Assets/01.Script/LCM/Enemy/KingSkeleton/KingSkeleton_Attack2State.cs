using UnityEngine;

public class KingSkeleton_Attack2State : EnemyState
{
    private float mass;
    public KingSkeleton_Attack2State(Enemy enemy) : base(enemy, EnemyStateType.Attack2.ToString())
    {
    }

    protected override void EnterState()
    {
        base.EnterState();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
        mass = _enemy.RbCompo.mass;
        _enemy.RbCompo.mass = 100;
        _enemy.Attakc2();
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
