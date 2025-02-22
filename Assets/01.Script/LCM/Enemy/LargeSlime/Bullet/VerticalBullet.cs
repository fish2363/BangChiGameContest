using UnityEngine;
using UnityEngine.Events;

public class VerticalBullet : MonoBehaviour, IPoolable
{
    [SerializeField] private string _poolName;

    public string PoolName => _poolName;
    public GameObject ObjectPrefab => gameObject;
    
    [SerializeField] private float _maxHeight = 10f;  
    

    private Rigidbody2D _rbCompo;
    
    private Animator _animator;

    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private float _explosionRadius;

    private readonly int _animHash = Animator.StringToHash("yMove");
    
    public UnityEvent OnExplode;
    [SerializeField] private float _damage;
    [SerializeField] private Vector2 _knockbackForce;

    private void Awake()
    {
        _rbCompo = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    public void ResetItem()
    {
        
    }

    public void ThrowObject(Vector2 targetPosition)
    {
        Vector2 startPosition = transform.position;
        float distanceX = targetPosition.x - startPosition.x;
        float distanceY = targetPosition.y - startPosition.y;

        float effectiveGravity = Mathf.Abs(Physics2D.gravity.y * _rbCompo.gravityScale);

        // 초기 속도 계산
        float timeToPeak = Mathf.Sqrt(2 * _maxHeight / effectiveGravity);
        float totalFlightTime = timeToPeak + Mathf.Sqrt(2 * (_maxHeight - distanceY) / effectiveGravity);

        float velocityX = distanceX / totalFlightTime;
        float velocityY = effectiveGravity * timeToPeak;

        _rbCompo.linearVelocity = new Vector2(velocityX, velocityY);
    }

    private void Update()
    {
        _animator.SetFloat(_animHash, _rbCompo.linearVelocityY);
        RotateAlongTrajectory();
    }

    private void RotateAlongTrajectory()
    {
        float angle;
        if (_rbCompo.linearVelocity != Vector2.zero)
        {
            if (_rbCompo.linearVelocityY > 0)
            {
                angle = Mathf.Atan2(_rbCompo.linearVelocityY, _rbCompo.linearVelocityX) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
            }
            else
            {
                angle = Mathf.Atan2(_rbCompo.linearVelocityY, _rbCompo.linearVelocityX) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle + 90f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("충돌");
            OnExplode?.Invoke();
            var player = Physics2D.OverlapCircle(transform.position, _explosionRadius, _whatIsPlayer);
            if (player != null)
            {
                player.GetComponentInChildren<EntityHealth>().ApplyDamage(_damage, transform.position, _knockbackForce,false,null);
            }
            PoolManager.Instance.Push(this);
        }
        
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
#endif
}
