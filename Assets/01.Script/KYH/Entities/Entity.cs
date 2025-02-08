using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour,IDamageable
{
    public delegate void OnDamageHandler(float damage, Vector2 direction, Vector2 knockBackPower, bool isPowerAttack, Entity dealer);
    public event OnDamageHandler OnDamage;

    public UnityEvent OnHit;
    public UnityEvent OnDead;

    public bool IsDead { get; set; } //���ó�� üũ�� ���� �� �س���
    public int DeadBodyLayer { get; private set; }

    protected Dictionary<Type, IEntityComponent> _components;

    protected virtual void Awake()
    {
        DeadBodyLayer = LayerMask.NameToLayer("DeadBody"); //���̾� �� ����

        _components = new Dictionary<Type, IEntityComponent>();
        AddComponentToDictionary();
        ComponentInitialize();
        AfterInitialize();
    }

    protected virtual void AddComponentToDictionary()
    {
        GetComponentsInChildren<IEntityComponent>(true).ToList().ForEach(compo => _components.Add(compo.GetType(), compo));
    }

    protected virtual void ComponentInitialize()
    {
        _components.Values.ToList().ForEach(compo => compo.Initialize(this));
    }

    protected virtual void AfterInitialize()
    {
        _components.Values.OfType<IAfterInit>().ToList().ForEach(compo => compo.AfterInitialize());
        OnHit.AddListener(HandleHit);
        OnDead.AddListener(HandleDead);
    }

    protected virtual void OnDestroy()
    {
        OnHit.RemoveListener(HandleHit);
        OnDead.RemoveListener(HandleDead);
    }

    protected abstract void HandleHit();
    protected abstract void HandleDead();

    public T GetCompo<T>(bool isDerived /*����Ŭ���� Ž�� ����*/= false) where T : IEntityComponent //�ݵ�� IEntityComponent�� �����ؾ� ��
    {
        if (_components.TryGetValue(typeof(T), out IEntityComponent component))
            return (T)component;//GetCompo�� ���� ���� ���׸��� ��ųʸ��� �߰��Ǿ� �ִ��� �˻� �� ������ ��ȯ

        if (isDerived == false) return default(T);//���� Ŭ������ ������ ������ �� �⺻�� ������ = ��?

        Type findType = _components.Keys.FirstOrDefault(type => type.IsSubclassOf(typeof(T)));//T�� �Ļ�Ŭ������ �ִ��� �˻��ϰ� ������ ���� ù��°���� ����
        if (findType != null)
            return (T)_components[findType];//ã������ �װ� ����

        return default(T);//���� �� ����
    }

    public void ApplyDamage(float damage, Vector2 direction, Vector2 knockBackPower, bool isPowerAttack, Entity dealer)
            => OnDamage?.Invoke(damage, direction, knockBackPower, isPowerAttack, dealer);
}
