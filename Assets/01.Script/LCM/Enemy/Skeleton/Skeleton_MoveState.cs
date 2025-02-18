using UnityEngine;

public class Skeleton_MoveState : EnemyState
{
    public Skeleton_MoveState(Enemy enemy) : base(enemy, EnemyStateType.Move.ToString())
    {
    }
    
    public override void FixedUpdateState()
    {
        base.UpdateState();

        if (!_enemy.CanMove) return;

        if (_enemy.CanAttackPlayer())
        {
            _enemy.RandomAttack();
        }

        if (_enemy.CanTargetingPlayer() == false)
        {
            _enemy.TransitionState(EnemyStateType.Idle);
        }

        _enemy.TargetingPlayer();

        Vector2 moveDir = _enemy.GetMovementDirection();
        moveDir.Normalize();

        _enemy.EnemyRotation();
        _enemy.RbCompo.linearVelocityX = moveDir.x * _enemy.EnemyData.movementSpeed;
    }
}
