using UnityEngine;

public class DrawSlime_MoveState : EnemyState
{
    public DrawSlime_MoveState(Enemy enemy) : base(enemy)
    {
        
    }

    public override void FixedUpdateState()
    {
        base.UpdateState();
        _enemy.TargetingPlayer();
        
        Vector2 moveDir = _enemy.GetMovementDirection();
        moveDir.Normalize();
        
        _enemy.EnemyRotation();
        _enemy.RbCompo.linearVelocityX = moveDir.x * _enemy.EnemyData.movementSpeed;
        
        
        if (_enemy.CanAttackPlayer())
        {
            _enemy.TransitionState(EnemyStateType.Attack);
        }

        if (_enemy.CanTargetingPlayer() == false)
        {
            Debug.Log("Change Idle");
            _enemy.TransitionState(EnemyStateType.Idle);
        }
    }
}
