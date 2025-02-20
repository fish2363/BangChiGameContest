using UnityEngine;

public class BuffIcon_AttackState : EnemyState
{
    public BuffIcon_AttackState(Enemy enemy) : base(enemy, EnemyStateType.Attack.ToString())
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
