using UnityEngine;

public class Sprout_MoveState : EnemyState
{
    public Sprout_MoveState(Enemy enemy) : base(enemy, EnemyStateType.Move.ToString())
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
        
        
        if (_enemy.CanAttackRangePlayer())
        {
            _enemy.TransitionState(EnemyStateType.Attack);
        }

        if (_enemy.CanTargetingPlayer() == false)
        {
            _enemy.TransitionState(EnemyStateType.Idle);
        }
    }
}
