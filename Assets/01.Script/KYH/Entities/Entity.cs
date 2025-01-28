using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected Dictionary<Type, IEntityComponent> _components;

    protected virtual void Awake()
    {
        _components = new Dictionary<Type, IEntityComponent>();

        AddComponentToDictionary();
        ComponentInitialize();
        AfterInitialize();
    }
    
    private void AddComponentToDictionary()
    {
        GetComponentsInChildren<IEntityComponent>(true).ToList().ForEach(compo => _components.Add(compo.GetType(),compo));
    }

    private void ComponentInitialize()
    {
        _components.Values.ToList().ForEach(compo => compo.Initialize(this));
    }

    private void AfterInitialize()
    {
        _components.Values.OfType<AfterInit>().ToList().ForEach(compo => compo.AfterInitialize());
    }

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
}
