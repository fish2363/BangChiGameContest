using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
    [field: SerializeField] public EnemyDataSO EnemyData;
    
    public Rigidbody2D RbCompo { get; private set; }
    public Animator AnimatorCompo { get; private set; }
    protected Dictionary<EnemyStateType, EnemyState> StateEnum = new Dictionary<EnemyStateType, EnemyState>();
    private EnemyStateType currentState;

    private float _currentScaleX;
    
    public Transform TargetTrm { get; private set; }
    
    public UnityEvent OnDeadEvent;
    
    
    public virtual void Awake()
    {
        RbCompo = GetComponent<Rigidbody2D>();
        AnimatorCompo = GetComponentInChildren<Animator>();
        _currentScaleX = transform.localScale.x;
    }
    
    public void TransitionState(EnemyStateType newState)
    {
        StateEnum[currentState].Exit();
        currentState = newState;
        StateEnum[currentState].Enter();
    }


    private void Update() => StateEnum[currentState].UpdateState();

    private void FixedUpdate() => StateEnum[currentState].FixedUpdateState();

    public Vector2 GetMovementDirection()
    {
        return TargetTrm.position - transform.position;
    }

    public void TargetingPlayer()
    {
        if (CanTargetingPlayer())
        {
            var target = Physics2D.OverlapCircle(transform.position, EnemyData.targetingRange,
                EnemyData.whatIsPlayer);
            TargetTrm = target.transform;
        }
    }

    public bool CanTargetingPlayer()
    {
        return Physics2D.OverlapCircle(transform.position, EnemyData.targetingRange, EnemyData.whatIsPlayer);
    }

    public bool CanAttackPlayer()
    {
        return Physics2D.OverlapCircle(transform.position, EnemyData.attackRange, EnemyData.whatIsPlayer);
    }

    public void EnemyRotation()
    {
        if (TargetTrm.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(_currentScaleX, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(_currentScaleX * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    public abstract void Attack();
    public abstract void Dead();
    
    
#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, EnemyData.targetingRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, EnemyData.attackRange);
        Gizmos.color = Color.white;
    }
#endif
}

public enum EnemyStateType
{
    Idle,
    Move,
    Attack,
    Dead
}
