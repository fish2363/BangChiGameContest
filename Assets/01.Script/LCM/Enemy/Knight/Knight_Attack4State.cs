using UnityEngine;

public class Knight_Attack4State : EnemyState
{
    private float mass;
    public Knight_Attack4State(Enemy enemy) : base(enemy, EnemyStateType.Attack4.ToString())
    {
    }
    
    protected override void EnterState()
    {
        base.EnterState();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
        _enemy.Attakc4();
        mass = _enemy.RbCompo.mass;
        _enemy.RbCompo.mass = 100;
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
