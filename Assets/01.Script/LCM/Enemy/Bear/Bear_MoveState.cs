using UnityEngine;

public class Bear_MoveState : EnemyState
{
    public Bear_MoveState(Enemy enemy, string animBoolHash) : base(enemy, EnemyStateType.Move.ToString())
    {
    }
    public override void FixedUpdateState()
    {
        base.UpdateState();
        if (!_enemy.CanMove) return;
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
            _enemy.TransitionState(EnemyStateType.Idle);
        }
    }
}
