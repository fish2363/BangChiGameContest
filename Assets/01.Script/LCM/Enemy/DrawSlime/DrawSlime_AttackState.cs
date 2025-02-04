using UnityEngine;

public class DrawSlime_AttackState : EnemyState
{
    public DrawSlime_AttackState(Enemy enemy) : base(enemy, EnemyStateType.Attack.ToString())
    {
        
    }

    protected override void EnterState()
    {
        base.EnterState();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
        _enemy.Attack();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
    }
}
