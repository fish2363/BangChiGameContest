using UnityEngine;

public class Bear_IdleState : EnemyState
{
    public Bear_IdleState(Enemy enemy, string animBoolHash) : base(enemy, EnemyStateType.Idle.ToString())
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
        if (_enemy.CanTargetingPlayer())
        {
            _enemy.TransitionState(EnemyStateType.Move);
        }
    }
}
