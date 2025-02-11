using System;
using UnityEngine;

public class EntityHealth : MonoBehaviour, IEntityComponent, IAfterInit
{
    public float maxHealth;
    public float _currentHealth { get; private set; }

    public event Action<Vector2> OnKnockback;

    private Entity _entity;
    private EntityFeedbackData _feedbackData;
    [HideInInspector] public NotifyValue<float> hp = new();

    #region Initialize section

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _feedbackData = _entity.GetCompo<EntityFeedbackData>();
        _currentHealth = maxHealth;
    }

    public void AfterInitialize()
    {
        _entity.OnDamage += ApplyDamage;
    }

    private void OnDestroy()
    {
        _entity.OnDamage -= ApplyDamage;
    }

    #endregion

    public void ApplyDamage(float damage, Vector2 direction, Vector2 knockBackPower, bool isPowerAttack, Entity dealer)
    {
        print("生焼たたたたたたたたたたたたたたたたたたたたたたたたた");
        if (_entity.IsDead) return; //戚耕 宋精 橿汐脊艦陥.

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
        _feedbackData.LastAttackDirection = direction.normalized;
        _feedbackData.IsLastHitPowerAttack = isPowerAttack;
        _feedbackData.LastEntityWhoHit = dealer;

        hp.Value = _currentHealth;

        AfterHitFeedbacks(knockBackPower);
    }

    private void AfterHitFeedbacks(Vector2 knockBackPower)
    {
        _entity.OnHit?.Invoke();
        OnKnockback?.Invoke(knockBackPower);
        if (_currentHealth <= 0)
        {
            _entity.OnDead?.Invoke();
        }
    }

}