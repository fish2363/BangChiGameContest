using UnityEngine;

public class DrawSlime_IdleState : EnemyState
{
    public DrawSlime_IdleState(Enemy enemy) : base(enemy, "Idle")
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
