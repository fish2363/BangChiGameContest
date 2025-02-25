using System;
using UnityEngine;

public class DashAttackState : EntityState
{
    private Player _player;
    private EntityMover _mover;
    private PlayerAttackCompo _attackCompo;

    public DashAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = entity.GetCompo<EntityMover>();
        _attackCompo = entity.GetCompo<PlayerAttackCompo>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.CanManualMove = false;

        _player.TurnOnTimer();

        SetAttackData();
    }

    private void SetAttackData()
    {
        AttackDataSO attackData = _attackCompo.GetAttackData("PlayerDashAttack");
        Vector2 movement = attackData.movement; //�̵� ���鲲
        movement.x *= _renderer.FacingDirection;
        _mover.AddForceToEntity(movement);
        _mover.EffectorPlayer.PlayEffect("DashAttackEffect");
        _attackCompo.SetAttackData(attackData);
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
            _player.ChangeState("IDLE");
    }

    public override void Exit()
    {
        if(!_player.isDialogue)
        _mover.CanManualMove = true;

        base.Exit();
    }
}
