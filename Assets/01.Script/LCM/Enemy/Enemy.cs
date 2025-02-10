using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : Entity
{
    [field: SerializeField] public EnemyDataSO EnemyData;
    
    public Rigidbody2D RbCompo { get; private set; }
    public Animator AnimatorCompo { get; private set; }
    protected Dictionary<EnemyStateType, EnemyState> StateEnum = new Dictionary<EnemyStateType, EnemyState>();
    private EnemyStateType currentState;

    private float _currentScaleX;

    public bool isAttackAnimationEnd { get; set; } = false;
    
    protected EntityAnimationTrigger AnimTriggerCompo  { get; private set; }
    
    public Transform TargetTrm { get; private set; }
    
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Transform _groundChecker;

    public bool CanMove { get; set; } = true; //넉백당하거나 기절시 이동불가

    protected override void Awake()
    {
        base.Awake();
        RbCompo = GetComponent<Rigidbody2D>();
        AnimatorCompo = GetComponentInChildren<Animator>();
        AnimTriggerCompo = transform.Find("Visual").GetComponent<EntityAnimationTrigger>();
        _currentScaleX = transform.localScale.x;
    }

    private void Start()
    {
        AnimTriggerCompo.OnAnimationEnd += () => isAttackAnimationEnd = true;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        // ReSharper disable once EventUnsubscriptionViaAnonymousDelegate
        AnimTriggerCompo.OnAnimationEnd -= () => isAttackAnimationEnd = true;
    }

    public void TransitionState(EnemyStateType newState)
    {
        StateEnum[currentState].Exit();
        currentState = newState;
        StateEnum[currentState].Enter();
    }

    public bool GroundCheck()
    {
        return Physics2D.OverlapBox(_groundChecker.position, EnemyData.groundCheckerBoxSize, 0, _whatIsGround);
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

    private float lastAttackTime = -Mathf.Infinity;

    public bool CanAttackPlayer()
    {
        bool isPlayerInRange = Physics2D.OverlapCircle(transform.position, EnemyData.attackRange, EnemyData.whatIsPlayer);

        bool isCooldownOver = Time.time >= lastAttackTime + EnemyData.attackCoolTime;

        if (isPlayerInRange && isCooldownOver)
        {
            lastAttackTime = Time.time;
            return true;
        }
    
        return false; // 공격 불가능
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

    private void HandleAttackAnimationEnd()
    {
        isAttackAnimationEnd = true;
    }

    public abstract void Attack();
    public abstract void Dead();

    public void AddForceToEntity(Vector2 force)
            => RbCompo.AddForce(force, ForceMode2D.Impulse);
    public void StopImmediately(bool isYAxisToo)
    {
        if (isYAxisToo)
            RbCompo.linearVelocity = Vector2.zero;
        else
            RbCompo.linearVelocityX = 0;
    }

    public void KnockBack(Vector2 force, float time)
    {
        CanMove = false;
        StopImmediately(true);
        AddForceToEntity(force);
        DOVirtual.DelayedCall(time, () => CanMove = true);
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, EnemyData.targetingRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, EnemyData.attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_groundChecker.position, EnemyData.groundCheckerBoxSize);
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
