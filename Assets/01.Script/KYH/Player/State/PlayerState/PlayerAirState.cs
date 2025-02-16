using UnityEngine;

public class PlayerAirState : EntityState
{
    protected Player _player;
    protected EntityMover _mover;

    public PlayerAirState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = entity.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.SetMoveSpeedMultiplier(0.7f);
        _player.PlayerInput.OnJumpKeyPressed += HandleAirJump;
        _player.PlayerInput.OnAttackKeyPressed += HandleAirAttack;
    }

    public override void Update()
    {
        base.Update();
        float xInput = _player.PlayerInput.InputDirection.x;
        if (Mathf.Abs(xInput) > 0)
            _mover.SetMovementX(xInput);

        bool isFrontMove = Mathf.Abs(xInput + _renderer.FacingDirection) > 1;
        if (isFrontMove && _mover.IsWallDetected(_renderer.FacingDirection))
        {
            _player.ChangeState("WALL_SLIDE");
        }
    }

    public override void Exit()
    {
        _player.PlayerInput.OnAttackKeyPressed -= HandleAirAttack;
        _player.PlayerInput.OnJumpKeyPressed -= HandleAirJump;
        _mover.SetMoveSpeedMultiplier(1f);
        base.Exit();
    }
    private void HandleAirJump()
    {
        if (_player.CanJump)
            _player.ChangeState("JUMP");
    }

    private void HandleAirAttack()
    {
        if(!_player.isBannedAttack && !_player.isDialogue)
        _player.ChangeState("JUMP_ATTACK");
    }
}
