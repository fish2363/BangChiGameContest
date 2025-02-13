using UnityEngine;

public class Knight_RunState : EnemyState
{
    private float _moveDistance = 4f;
    private float _runSpeed = 3f;
    public Knight_RunState(Enemy enemy) : base(enemy, EnemyStateType.Run.ToString())
    {
    }
    
    public override void FixedUpdateState()
    {
        base.UpdateState();

        if (!_enemy.CanMove) return;

        if (_enemy.CanTargetingPlayer() == false)
        {
            _enemy.TransitionState(EnemyStateType.Idle);
        }
        _enemy.IsCanShield();

        _enemy.TargetingPlayer();
        
        if (Vector2.Distance(_enemy.TargetTrm.position, _enemy.transform.position) < _moveDistance)
        {
            _enemy.TransitionState(EnemyStateType.Move);
        }


        Vector2 moveDir = _enemy.GetMovementDirection();
        moveDir.Normalize();

        _enemy.EnemyRotation();
        _enemy.RbCompo.linearVelocityX = moveDir.x * (_enemy.EnemyData.movementSpeed + _runSpeed);
    }
}
