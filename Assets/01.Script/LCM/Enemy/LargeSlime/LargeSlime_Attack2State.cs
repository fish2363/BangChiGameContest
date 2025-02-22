using UnityEngine;

public class LargeSlime_Attack2State : EnemyState
{
    private float mass;
    public LargeSlime_Attack2State(Enemy enemy) : base(enemy, EnemyStateType.Attack2.ToString())
    {
    }
    
    protected override void EnterState()
    {
        base.EnterState();
        mass = _enemy.RbCompo.mass;
        _enemy.RbCompo.mass = 100;
        _enemy.RbCompo.linearVelocity = Vector2.zero;
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
        _enemy.RbCompo.mass = mass;
        _enemy.isAttackAnimationEnd = false;
    }
}
