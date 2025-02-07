using UnityEngine;

public abstract class DamageCaster : MonoBehaviour, ICounterable
{

    [SerializeField] protected int maxHitCount = 1; //�ִ� �ǰ� ���� ��ü ��
    [SerializeField] protected ContactFilter2D contactFilter;

    protected Entity _owner;

    public virtual void InitCaster(Entity owner)
    {
        _owner = owner;
    }

    public abstract bool CastDamage(float damage, Vector2 knockBack, bool isPowerAttack);

    public bool CanCounter { get; set; }
    public Transform TargetTrm => _owner.transform;

    public abstract void ApplyCounter(float damage, Vector2 direction, Vector2 knockBackForce, bool isPowerAttack,
        Entity dealer);

    public abstract Collider2D GetCounterableTarget(Vector3 center, LayerMask whatIsCounterable);
}
