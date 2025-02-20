using UnityEngine;

public class BuffIcon_Attack3State : EnemyState
{
    public BuffIcon_Attack3State(Enemy enemy) : base(enemy, EnemyStateType.Attack3.ToString())
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
