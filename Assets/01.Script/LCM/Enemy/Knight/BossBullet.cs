using UnityEngine;
using UnityEngine.Events;

public class BossBullet : MonoBehaviour, IPoolable
{
    [SerializeField] private string _poolName;
    public string PoolName => _poolName;
    public GameObject ObjectPrefab => gameObject;


    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;


    [SerializeField] private float _bulletLifeTime;
    private float _curTime;

    [SerializeField] private float _damage;
    [SerializeField] private Vector2 _knockbackForce;

    [SerializeField] private string _bulletAudio;

    private Vector2 _moveDir;

    public UnityEvent OnDeadEvent;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 moveDir)
    {
        _moveDir = moveDir;
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
            AudioManager.Instance.PlaySound2D(_bulletAudio, 0, false, SoundType.SfX);
            PoolManager.Instance.Push(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponentInChildren<EntityHealth>()
                .ApplyDamage(_damage, transform.position, _knockbackForce, false, null);
            OnDeadEvent?.Invoke();
            if (_bulletAudio.Length > 0)
                AudioManager.Instance.PlaySound2D(_bulletAudio, 0, false, SoundType.SfX);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            OnDeadEvent?.Invoke();
            if (_bulletAudio.Length > 0)
                AudioManager.Instance.PlaySound2D(_bulletAudio, 0, false, SoundType.SfX);
        }

        PoolManager.Instance.Push(this);
    }

    public void ResetItem()
    {
        _curTime = 0;
    }
}