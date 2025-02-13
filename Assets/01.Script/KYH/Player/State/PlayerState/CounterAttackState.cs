using UnityEngine;

public class CounterAttackState : EntityState
{
    private Player _player;
    private PlayerAttackCompo _attackCompo;
    private EntityMover _mover;

    private float _counterTimer;
    private bool _counterSuccess;

    public CounterAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _attackCompo = entity.GetCompo<PlayerAttackCompo>();
        _mover = entity.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(false);
        _counterTimer = _attackCompo.counterAttackDuration;
        _renderer.SetParam(_attackCompo.successCounterParam, false);
        _counterSuccess = false;
        _mover.EffectorPlayer.PlayEffect("CounterWait");
}

public override void Update()
{
    base.Update();
    _counterTimer -= Time.deltaTime;
    if (_counterSuccess == false)
        CheckCounter();

    if (_counterTimer < 0 || _isTriggerCall)
    {
            _mover.EffectorPlayer.StopEffect("CounterWait");
        _player.ChangeState("IDLE");
    }
}

    private void CheckCounter()
    {
        ICounterable countable = _attackCompo.GetCounterableTargetInRadius();
        Debug.Log(countable is { CanCounter: true });

        if (countable is { CanCounter: true })
        {
            _mover.EffectorPlayer.StopEffect("CounterWait");
            _mover.EffectorPlayer.PlayEffect("CounterEffect", true);
        _counterSuccess = true;
            AttackDataSO attackData = _attackCompo.GetAttackData("PlayerCounterAttack");
            float damage = 10f; //�ϵ��ڵ�
            Vector2 attackDirection = new Vector2(_renderer.FacingDirection, 0);
            Vector2 knockBackForce = attackData.knockBackForce;
            knockBackForce.x *= _renderer.FacingDirection;

            countable.ApplyCounter(damage, attackDirection, knockBackForce, attackData.isPowerAttack, _player);
            _renderer.SetParam(_attackCompo.successCounterParam, true);

            //ī���� �����޽��� ������.
            CounterSuccessEvent counterEvt = PlayerEvents.CounterSuccessEvent;
            counterEvt.target = countable.TargetTrm;
            _player.PlayerChannel.RaiseEvent(counterEvt);
        }
    }
}