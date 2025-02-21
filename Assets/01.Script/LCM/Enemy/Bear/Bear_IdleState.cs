using UnityEngine;

public class Bear_IdleState : EnemyState
{
    public Bear_IdleState(Enemy enemy) : base(enemy, EnemyStateType.Idle.ToString())
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
        if (_enemy.CanTargetingPlayer() && !_enemy.CanAttackRangePlayer())
        {
            _enemy.TransitionState(EnemyStateType.Move);
        }

        if (_enemy.CanAttackCoolTime() && _enemy.CanAttackRangePlayer())
        {
            _enemy.RandomAttack();
        }
        
        _enemy.TargetingPlayer();
        if (_enemy.TargetTrm != null)
        {
            _enemy.EnemyRotation();
        }
    }
}
