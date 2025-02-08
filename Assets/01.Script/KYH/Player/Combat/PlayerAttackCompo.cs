using System.Collections.Generic;
using UnityEngine;


public class PlayerAttackCompo : MonoBehaviour, IEntityComponent, IAfterInit
{
    [SerializeField] private DamageCaster damageCaster;

    [SerializeField] private List<AttackDataSO> attackDataList;

    
    private Player _player;
    private EntityRenderer _renderer;
    private EntityMover _mover;
    private EntityAnimationTrigger _triggerCompo;

    private bool _canJumpAttack;

    private Dictionary<string, AttackDataSO> _attackDataDictionary;
    private AttackDataSO _currentAttackData;

    #region Init section

    public void Initialize(Entity entity)
    {
        _player = entity as Player;
        _renderer = entity.GetCompo<EntityRenderer>();
        _mover = entity.GetCompo<EntityMover>();
        _triggerCompo = entity.GetCompo<EntityAnimationTrigger>();
        damageCaster.InitCaster(entity);

        //리스트를 딕셔너리로 변경한다.
        _attackDataDictionary = new Dictionary<string, AttackDataSO>();
        attackDataList.ForEach(attackData => _attackDataDictionary.Add(attackData.attackName, attackData));
    }


    private void OnDestroy()
    {
        _triggerCompo.OnAttackTrigger -= HandleAttackTrigger;
    }
    #endregion



    public bool CanJumpAttack()
    {
        bool returnValue = _canJumpAttack;
        if (_canJumpAttack)
            _canJumpAttack = false;
        return returnValue;
    }

    private void FixedUpdate()
    {
        if (_canJumpAttack == false && _mover.IsGroundDetected())
            _canJumpAttack = true;
    }

    public AttackDataSO GetAttackData(string attackName)
    {
        AttackDataSO data = _attackDataDictionary.GetValueOrDefault(attackName);
        Debug.Assert(data != null, $"request attack data is not exist : {attackName}");
        return data;
    }

    

    public void SetAttackData(AttackDataSO attackData)
    {
        _currentAttackData = attackData;
    }
    

    private void HandleAttackTrigger()
    {
        float damage = 5f; //나중에 스탯기반으로 고침. 
        Vector2 knockBackForce = _currentAttackData.knockBackForce;
        Debug.Log($"{damageCaster},{_currentAttackData}");
        bool success = damageCaster.CastDamage(damage, knockBackForce, _currentAttackData.isPowerAttack);

        if (success)
        {
            Debug.Log($"<color=red>Damaged! - {damage}</color>");
        }
    }


    public void AfterInitialize()
    {
        _triggerCompo.OnAttackTrigger += HandleAttackTrigger;
    }
}
