using UnityEngine;
using UnityEngine.Events;

public class BossBullet : Entity, IPoolable
{
    [SerializeField] private string _poolName;
    public string PoolName => _poolName;
    public GameObject ObjectPrefab => gameObject;

    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private float _radius;

    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;


    [SerializeField] private float _bulletLifeTime;
    private float _curTime;

    [SerializeField] private float _damage;
    [SerializeField] private Vector2 _knockbackForce;

    private Vector2 _moveDir;

    public UnityEvent OnDeadEvent;
    
    private Knight _boss;


    protected override void Awake()
    {
        base.Awake();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boss = FindAnyObjectByType<Knight>();
    }

    protected override void HandleHit()
    {
    }

    protected override void HandleDead()
    {
    }


    private void OnEnable()
    {
        transform.position = _boss.transform.position;
        if (Physics2D.OverlapCircle(transform.position, _radius, _whatIsPlayer))
        {
            var player = Physics2D.OverlapCircle(transform.position, _radius, _whatIsPlayer);
            _moveDir = new Vector2(player.transform.position.x - transform.position.x, 0).normalized;
            Debug.Log(transform.position);
        }
    }


    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = _moveDir * _speed;
    }

    private void Update()
    {
        _curTime += Time.deltaTime;
        if (_curTime >= _bulletLifeTime)
        {
            OnDeadEvent?.Invoke();
            PoolManager.Instance.Push(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponentInChildren<EntityHealth>()
                .ApplyDamage(_damage, transform.position, _knockbackForce, false, this);
            OnDeadEvent?.Invoke();
            PoolManager.Instance.Push(this);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            OnDeadEvent?.Invoke();
            PoolManager.Instance.Push(this);
        }
    }

    public void ResetItem()
    {
        _curTime = 0;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
        Gizmos.color = Color.white;
    }
#endif
}