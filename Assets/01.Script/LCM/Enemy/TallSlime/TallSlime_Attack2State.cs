using UnityEngine;

public class TallSlime_Attack2State : EnemyState
{
    public TallSlime_Attack2State(Enemy enemy) : base(enemy, EnemyStateType.Attack2.ToString())
    {
    }
    
    protected override void EnterState()
    {
        base.EnterState();
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
        _enemy.isAttackAnimationEnd = false;
    }
}
