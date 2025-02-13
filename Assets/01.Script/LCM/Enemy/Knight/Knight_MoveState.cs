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

        if (_enemy.CanAttackPlayer())
        {
            _enemy.TransitionState(EnemyStateType.Attack3);
        }
        _enemy.IsCanShield();
        
        _enemy.TargetingPlayer();

        if (Vector2.Distance(_enemy.TargetTrm.position, _enemy.transform.position) > _runDistance)
        {
            _enemy.TransitionState(EnemyStateType.Run);
        }


        Vector2 moveDir = _enemy.GetMovementDirection();
        moveDir.Normalize();

        _enemy.EnemyRotation();
        _enemy.RbCompo.linearVelocityX = moveDir.x * _enemy.EnemyData.movementSpeed;
    }
}
