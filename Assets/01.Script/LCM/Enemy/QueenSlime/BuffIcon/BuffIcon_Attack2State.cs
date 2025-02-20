using UnityEngine;

public class BuffIcon_Attack2State : EnemyState
{
    public BuffIcon_Attack2State(Enemy enemy) : base(enemy, EnemyStateType.Attack2.ToString())
    {
    }
    
    public override void UpdateState()
    {
        base.UpdateState();
        if(_enemy.isAttackAnimationEnd)
            _enemy.TransitionState(EnemyStateType.Idle);
    }

    protected override void ExtiState()
    {
        base.ExtiState();
        _enemy.isAttackAnimationEnd = false;
    }
}
