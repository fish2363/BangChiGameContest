using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour,IDamageable
{
    public delegate void OnDamageHandler(float damage, Vector2 direction, Vector2 knockBackPower, bool isPowerAttack, Entity dealer);
    public event OnDamageHandler OnDamage;
    protected Dictionary<Type, IEntityComponent> _components;

    protected virtual void Awake()
    {
        _components = new Dictionary<Type, IEntityComponent>();

        AddComponentToDictionary();
        ComponentInitialize();
        AfterInitialize();
    }

    protected virtual void AddComponentToDictionary()
    {
        GetComponentsInChildren<IEntityComponent>(true).ToList().ForEach(compo => _components.Add(compo.GetType(),compo));
    }

    protected virtual void ComponentInitialize()
    {
        _components.Values.ToList().ForEach(compo => compo.Initialize(this));
    }

    protected virtual void AfterInitialize()
    {
        _components.Values.OfType<IAfterInit>().ToList().ForEach(compo => compo.AfterInitialize());
    }

    public T GetCompo<T>(bool isDerived /*서브클래스 탐색 여부*/= false) where T : IEntityComponent //반드시 IEntityComponent를 구현해야 함
    {
        if (_components.TryGetValue(typeof(T), out IEntityComponent component))
            return (T)component;//GetCompo를 통해 들어온 제네릭이 딕셔너리에 추가되어 있는지 검사 후 있으면 반환

        if (isDerived == false) return default(T);//서브 클래스를 원하지 않으면 걍 기본값 내보냄 = 널?

        Type findType = _components.Keys.FirstOrDefault(type => type.IsSubclassOf(typeof(T)));//T의 파생클래스가 있는지 검사하고 있으면 가장 첫번째꺼를 뱉음
        if (findType != null)
            return (T)_components[findType];//찾았으면 그걸 뱉음

        return default(T);//없음 널 뱉음
    }

    public void ApplyDamage(float damage, Vector2 direction, Vector2 knockBackPower, bool isPowerAttack, Entity dealer)
            => OnDamage?.Invoke(damage, direction, knockBackPower, isPowerAttack, dealer);
}
