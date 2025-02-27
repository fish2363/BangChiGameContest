using UnityEngine;

public class AttackState : EntityState
{
    private Player _player;
    private EntityMover _mover;
    private PlayerAttackCompo _attackCompo;

    private int _comboCounter;
    private float _lastAttackTime;
    private readonly float _comboWindow = 0.8f; //�޺��� �̾������� �ϴ� �ð�����
    private const int MAX_COMBO_COUNT = 2;

    private bool isAlreadyAttackLock;

    public AttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = entity.GetCompo<EntityMover>();
        _attackCompo = entity.GetCompo<PlayerAttackCompo>();
    }

    public override void Enter()
    {
        base.Enter();
        //�ִ� �޺��� �����߰ų�, �����������κ��� �޺� ������ð� �̻� �귶�ٸ� �޺� �ʱ�ȭ
        if (_comboCounter > MAX_COMBO_COUNT || Time.time >= _lastAttackTime + _comboWindow)
            _comboCounter = 0;

        _renderer.SetParam(_player.ComboCounterParam, _comboCounter);

        if (!_mover.CanManualMove)
            isAlreadyAttackLock = true;
        else
            _mover.CanManualMove = false; //�������� ���ϰ�
        _mover.StopImmediately(true);

        SetAttackData();
    }

    private void SetAttackData()
    {
        float atkDirection = _renderer.FacingDirection;
        float xInput = _player.PlayerInput.InputDirection.x;

        if (Mathf.Abs(xInput) > 0)
            atkDirection = Mathf.Sign(xInput);

        AttackDataSO attackData = _attackCompo.GetAttackData($"PlayerCombo{_comboCounter}");
        _mover.EffectorPlayer.PlayEffect($"Combo{_comboCounter}AttackEffect");
        if(_comboCounter == 2)
        {
            AudioManager.Instance.PlaySound2D($"SwordAttack{_comboCounter}", 0, false, SoundType.SfX);
            AudioManager.Instance.PlaySound2D($"SwordAttack{0}", 0.5f, false, SoundType.SfX);
        }
        else
            AudioManager.Instance.PlaySound2D($"SwordAttack{_comboCounter}", 0, false, SoundType.SfX);
        Vector2 movement = attackData.movement;
        movement.x *= atkDirection;
        _mover.AddForceToEntity(movement);

        _attackCompo.SetAttackData(attackData); //���� ���� � ���ݵ����ͷ� �����Ұ��� ����
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
            _player.ChangeState("IDLE");
    }

    public override void Exit()
    {
        ++_comboCounter;
        _lastAttackTime = Time.time;
        if(!_player.isDialogue && !isAlreadyAttackLock)
        _mover.CanManualMove = true; //�̰� ������
        isAlreadyAttackLock = false;
        base.Exit();
    }
}
