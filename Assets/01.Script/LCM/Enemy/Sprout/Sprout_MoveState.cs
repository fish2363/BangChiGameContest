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

        if (_enemy.CanAttackRangePlayer() && _enemy.CanAttackCoolTime())
        {
            _enemy.TransitionState(EnemyStateType.Attack);
        }

        if (_enemy.CanTargetingPlayer() == false || (_enemy.CanAttackRangePlayer() && !_enemy.CanAttackCoolTime()))
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
