using UnityEngine;

public class Bear_MoveState : EnemyState
{
    public Bear_MoveState(Enemy enemy) : base(enemy, EnemyStateType.Move.ToString())
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

        if (!Physics2D.OverlapCircle(_enemy.transform.position, _enemy.EnemyData.attackRange,
                _enemy.EnemyData.whatIsPlayer))
        {
            _enemy.TargetingPlayer();
            
            Vector2 moveDir = _enemy.GetMovementDirection();
            moveDir.Normalize();
            
            _enemy.EnemyRotation();
            _enemy.RbCompo.linearVelocityX = moveDir.x * _enemy.EnemyData.movementSpeed;
        }
    }
}
