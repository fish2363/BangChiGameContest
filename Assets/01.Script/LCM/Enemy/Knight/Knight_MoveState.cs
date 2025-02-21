using UnityEngine;

public class Knight_MoveState : EnemyState
{
    private float _runDistance = 4f;
    public Knight_MoveState(Enemy enemy) : base(enemy, EnemyStateType.Move.ToString())
    {
    }
    
    public override void FixedUpdateState()
    {
        base.UpdateState();

        if (!_enemy.CanMove) return;

        if (_enemy.CanAttackRangePlayer() && _enemy.CanAttackCoolTime())
        {
            _enemy.RandomAttack();
        }

        if (_enemy.CanAttackRangePlayer() && !_enemy.CanAttackCoolTime())
        {
            _enemy.TransitionState(EnemyStateType.Idle);
        }
        
        _enemy.TargetingPlayer();

        if (Vector2.Distance(_enemy.TargetTrm.position, _enemy.transform.position) > _runDistance && !_enemy.isShield)
        {
            _enemy.TransitionState(EnemyStateType.Run);
        }


        Vector2 moveDir = _enemy.GetMovementDirection();
        moveDir.Normalize();

        _enemy.EnemyRotation();
        _enemy.RbCompo.linearVelocityX = moveDir.x * _enemy.EnemyData.movementSpeed;
        
        _enemy.IsCanShield();
    }
}
