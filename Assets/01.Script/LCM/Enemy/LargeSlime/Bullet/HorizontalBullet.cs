using System;
using UnityEngine;
using UnityEngine.Serialization;

public class HorizontalBullet : MonoBehaviour, IPoolable
{
    [SerializeField] private string _poolName;

    [SerializeField] private Transform _player;
    public string PoolName => _poolName;
    public GameObject ObjectPrefab => gameObject;
    
    [SerializeField] private float _maxHeight = 10f;  
    

    private Rigidbody2D rb;
    
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ResetItem()
    {
        
    }

    private void Start()
    {
        ThrowObject(_player.position);
    }
    

    public void ThrowObject(Vector2 targetPosition)
    {
        Vector2 startPosition = transform.position;
        float distanceX = targetPosition.x - startPosition.x;
        float distanceY = targetPosition.y - startPosition.y;

        float effectiveGravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);

        // 초기 속도 계산
        float timeToPeak = Mathf.Sqrt(2 * _maxHeight / effectiveGravity);
        float totalFlightTime = timeToPeak + Mathf.Sqrt(2 * (_maxHeight - distanceY) / effectiveGravity);

        float velocityX = distanceX / totalFlightTime;
        float velocityY = effectiveGravity * timeToPeak;

        rb.linearVelocity = new Vector2(velocityX, velocityY);
    }
}
