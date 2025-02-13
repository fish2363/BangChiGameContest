using System;
using UnityEngine;

public class EnemyAttackCompo : MonoBehaviour,IEntityComponent,IAfterInit
{
    [SerializeField] private DamageCaster damageCaster;
    private EntityAnimationTrigger _triggerCompo;

    [SerializeField]
    private float damage;
    [SerializeField]
    private Vector2 knockBackForce;

    public void Initialize(Entity entity)
    {
        _triggerCompo = entity.GetCompo<EntityAnimationTrigger>();
        damageCaster.InitCaster(entity);
    }

    public void AfterInitialize()
    {
        _triggerCompo.OnAttackTrigger += HandleAttackTrigger;
    }

    private void OnDestroy()
    {
        _triggerCompo.OnAttackTrigger -= HandleAttackTrigger;
    }

    private void HandleAttackTrigger()
    {
        bool success = damageCaster.CastDamage(damage, knockBackForce, false);

        if (success)
        {
            Debug.Log($"<color=red>Damaged! - {damage}</color>");
        }
    }
}
