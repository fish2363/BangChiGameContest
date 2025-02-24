using UnityEngine;
using UnityEngine.Events;

public abstract class VerticalBullet : MonoBehaviour, IPoolable
{
    [SerializeField] private string _poolName;

    public string PoolName => _poolName;
    public GameObject ObjectPrefab => gameObject;
    
    

    protected Rigidbody2D _rbCompo;
    
    [SerializeField] protected LayerMask _whatIsPlayer;
    

    [SerializeField] protected float _damage;
    [SerializeField] protected Vector2 _knockbackForce;

    protected virtual void Awake()
    {
        _rbCompo = GetComponent<Rigidbody2D>();
    }

    public virtual void ResetItem()
    {
        
    }

    public abstract void ThrowObject(Vector2 targetPosition);

    protected virtual void Update()
    {
        RotateAlongTrajectory();
    }

    protected abstract void RotateAlongTrajectory();
}
